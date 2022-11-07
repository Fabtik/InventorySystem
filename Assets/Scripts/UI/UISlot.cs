using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private UIItem _UIItem;
    public ISlot slot { get; private set; }
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
    }
    public void SetSlot(ISlot slot)
    {
        this.slot = slot;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var otherUIItem = eventData.pointerDrag.GetComponent<UIItem>();
        var otherUISlot = otherUIItem.GetComponentInParent<UISlot>();
        var otherSlot = otherUISlot.slot;

        var inventory = _inventory._inventory;
        inventory.DropFromSlotToSlot(this, otherSlot, slot);

        Refresh();
        otherUISlot.Refresh();
    }

    public void Refresh()
    {
        if(slot != null)
        {
            _UIItem.Refresh(slot);
        }
    }
}
