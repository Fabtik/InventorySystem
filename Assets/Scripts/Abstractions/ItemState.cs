using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemState : IItemState
{
    public int itemAmount;

    public int amount { get => itemAmount; set => itemAmount = value; }

    public ItemState()
    {
        itemAmount = 0;
    }
}
