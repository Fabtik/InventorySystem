using System;

public interface ISlot
{
    public bool isFull { get; }
    public bool isEmpty { get; }
    public IItem item {get;}
    public int itemID { get; }
    public int amount { get; }
    public int capacity { get; }

    public void SetItem(IItem item);
    public void Clear();
}
