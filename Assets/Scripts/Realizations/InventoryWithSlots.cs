 using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{

    public event Action<object, IItem, int> OnItemAddedEvent;
    public event Action<object, int, int> OnItemRemovedEvent;

    public event Action<object> OnInventoryChangedEvent;


    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => isFull);

    private List<ISlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<ISlot>(capacity);
        for(int i = 0; i < capacity; i++)
        {
            _slots.Add(new Slot());
        }
    }

    public ISlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

    public IItem GetItemWithID(int id) 
    {
        return _slots.Find(slot => slot.itemID == id).item;
    }
    public IItem[] GetAllItems() 
    {
        var allItems = new List<IItem>();

        foreach(var slot in _slots)
        {
            if (!slot.isEmpty)
            {
                allItems.Add(slot.item);
            }
        }
        return allItems.ToArray();
    }
    public IItem[] GetAllItemsWithID(int id) 
    {
        var allItemsWithID = new List<IItem>();

        var slotsWithItemWithID = _slots.FindAll(slot => !slot.isEmpty && slot.itemID == id);
        foreach (var slot in slotsWithItemWithID)
        {
            allItemsWithID.Add(slot.item);
        }
        return allItemsWithID.ToArray();
    }
    public int GetItemAmountWithID(int id) 
    {
        var amount = 0;
        var slotsWithItemWithID = _slots.FindAll(slot => !slot.isEmpty && slot.itemID == id);

        foreach(var slot in slotsWithItemWithID)
        {
            amount += slot.amount;
        }

        return amount;
    }

    public bool TryToAdd(object sender, IItem item) 
    {
        var slotWithSameItem = _slots.Find(slot => !slot.isEmpty && slot.itemID == item.data.ID && !slot.isFull);
        if(slotWithSameItem != null)
        {
            return TryToAddToSlot(sender, slotWithSameItem, item);
        }

        var emptySlot = _slots.Find(slot => slot.isEmpty);

        if(emptySlot != null)
        {
            return TryToAddToSlot(sender, emptySlot, item);
        }


       // Debug.Log("item with id - " + item.type + "amount - " + item.state.amount + "cannot be added");
        return false;
        
    }

    public bool TryToAddToSlot(object sender, ISlot slot, IItem item)
    {
        var fits = slot.amount + item.state.amount <= item.data.maxItemsInSlot;

        var needToAdd = fits ? item.state.amount : item.data.maxItemsInSlot - slot.amount;

        var leftToAdd = item.state.amount - needToAdd;

        var clone = item.Clone();
        clone.state.amount = needToAdd;

        if (slot.isEmpty)
        {
            slot.SetItem(clone);
        }
        else
        {
            slot.item.state.amount += needToAdd;
        }

        Debug.Log("item added to inventory. item - " + item.data.ID + " amount - " + needToAdd);
        OnItemAddedEvent?.Invoke(sender, item, needToAdd);
        OnInventoryChangedEvent?.Invoke(sender);


        if (leftToAdd <= 0)
        {
            return true;
        }

        item.state.amount = leftToAdd;
        return TryToAdd(sender, item);

    }

    public void DropFromSlotToSlot(object sender, ISlot fromSlot, ISlot toSlot)
    {
        if (fromSlot.isEmpty)
        {
            return;
        }

        if (toSlot.isFull)
        {
            return;
        }

        if(!toSlot.isEmpty && fromSlot.itemID != toSlot.itemID)
        {
            return;
        }

        var slotCapacity = fromSlot.capacity;
        var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        var needToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
        var leftAmount = fromSlot.amount - needToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();
            OnInventoryChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.amount += needToAdd;
        if (fits)
        {
            fromSlot.Clear();
        }
        else
        {
            fromSlot.item.state.amount = leftAmount;
        }
        OnInventoryChangedEvent?.Invoke(sender);
    }

    public void RemoveItemWithID(object sender, int id, int amount)
    {
        var slotsWithItemWithID = GetAllSlotsWithItemWithID(id);

        if(slotsWithItemWithID.Length == 0)
        {
            return;
        }

        var needToRemove = amount;
        var count = slotsWithItemWithID.Length;

        for(int i = count - 1; i >= 0; i--)
        {
            var slot = slotsWithItemWithID[i];

            if(slot.amount >= needToRemove)
            {
                slot.item.state.amount -= needToRemove;

                if(slot.amount <= 0)
                {
                    slot.Clear();
                }

                Debug.Log("item removed from inventory. itemid - " + id + " amount - " + needToRemove);
                OnItemRemovedEvent?.Invoke(sender, id, needToRemove);
                OnInventoryChangedEvent?.Invoke(sender);

                break;
            }

            var amountRemoved = slot.amount;
            needToRemove -= slot.amount;
            slot.Clear();

            Debug.Log("item removed from inventory. item - " + id + " amount - " + amountRemoved);
            OnItemRemovedEvent?.Invoke(sender, id, amountRemoved);
            OnInventoryChangedEvent?.Invoke(sender);
        }
    }

    public ISlot[] GetAllSlotsWithItemWithID(int id)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemID == id).ToArray();
    }

    public bool HasItemWithID(int id, out IItem item)
    {
        item = GetItemWithID(id);
        return item != null;
    }
}
