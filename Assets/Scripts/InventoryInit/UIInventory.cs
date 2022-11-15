using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UISlot[] _UISlots;


    public InventoryWithSlots _inventory { get; private set; }

    private void Awake()
    {
        _UISlots = GetComponentsInChildren<UISlot>();
        _inventory = new InventoryWithSlots(_UISlots.Length);
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

}
