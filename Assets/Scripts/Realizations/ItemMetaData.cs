using UnityEngine;

[CreateAssetMenu(fileName = "ItemMetaData", menuName = "MetaData/Create new ItemMetaData")]
public class ItemMetaData : ScriptableObject, IItemMetaData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _maxItemsInSlot;


    public int ID => _id; 

    public string Name => _name; 
    public string description => _description; 
    public Sprite sprite => _sprite; 

    public int maxItemsInSlot => _maxItemsInSlot; 
}
