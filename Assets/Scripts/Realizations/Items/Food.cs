using UnityEngine;

public class Food : UsableItem 
{
    public Food(IItemMetaData data) : base (data)
    {
        this.data = data;
        state = new ItemState();
    }

    public override IItem Clone()
    {
        var clone = new Food(this.data);
        clone.state.amount = this.state.amount;

        return clone;
    }

    public override void Use()
    {
        Debug.Log("You ate an " + this.data.Name);
    }
}
