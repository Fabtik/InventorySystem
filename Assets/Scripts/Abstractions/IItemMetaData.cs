using UnityEngine;
public interface IItemMetaData 
{
    public int ID { get; }

    public string Name { get; }
    public string description { get;  }
    public Sprite sprite { get; }

    public int maxItemsInSlot { get; }
 
}
