public interface IInventory 
{
    public int capacity { get; set; }
    public bool isFull { get; }

    public IItem GetItemWithID(int id);
    public IItem[] GetAllItems();
    public IItem[] GetAllItemsWithID(int id);
    public int GetItemAmountWithID(int id);

    public bool TryToAdd(object sender, IItem item);
    public void RemoveItemWithID(object sender, int id, int amount);
    public bool HasItemWithID(int id, out IItem item);
}
