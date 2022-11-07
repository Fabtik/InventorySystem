using System;

public interface IItem 
{
    public IItemMetaData data { get; }
    public IItemState state { get; }

    public IItem Clone();

}
