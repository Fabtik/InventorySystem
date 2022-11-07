using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private UISlot[] _UISlots;

    private MetaDataStorage _metaDataStorage;

    public InventoryWithSlots _inventory { get; private set; }

    private void Awake()
    {
        _metaDataStorage = GetComponent<MetaDataStorage>();

        _UISlots = GetComponentsInChildren<UISlot>();
        _inventory = new InventoryWithSlots(66);
        _inventory.OnInventoryChangedEvent += OnInventoryChanged;
        SetupInventoryUI(_inventory);
    }


    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _UISlots[i];

            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryChanged(object obj)
    {
        foreach (var slot in _UISlots)
        {
            slot.Refresh();
        }
    }





    //TEST!
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var item = new Item(_metaDataStorage.GetMetaDataWithID(0));
            item.state.amount = 1;

            _inventory.TryToAdd(this, item);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {            
            _inventory.RemoveItemWithID(this, 0, 1);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            var item = new Item(_metaDataStorage.GetMetaDataWithID(1));
            item.state.amount = 1;

            _inventory.TryToAdd(this, item);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _inventory.RemoveItemWithID(this, 1, 1);
        }
    }*/
}
