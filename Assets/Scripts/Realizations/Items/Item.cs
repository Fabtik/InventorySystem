using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    public Type type => GetType();

    public IItemMetaData data { get; }

    public IItemState state { get; }

    public Item(IItemMetaData data)
    {
        this.data = data;
        state = new ItemState();
    }

    public IItem Clone()
    {
        var clone = new Item(data);
        clone.state.amount = this.state.amount;

        return clone;
    }
}
