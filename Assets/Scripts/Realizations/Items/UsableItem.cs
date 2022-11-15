using UnityEngine;

public class UsableItem : IItem
{

    public IItemMetaData data { get; set; }

    public IItemState state { get; set; }

    public UsableItem(IItemMetaData data)
    {
        this.data = data;
        state = new ItemState();
    }

    public virtual IItem Clone()
    {
        var clone = new UsableItem(data);
        clone.state.amount = this.state.amount;

        return clone;
    }

    public virtual void Use()
    {
        Debug.LogWarning("You use an " + data.Name);
    }
}
