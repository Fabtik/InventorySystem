using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MetaDataStorage : MonoBehaviour
{
    [SerializeField] private List<ItemMetaData> _metaData;

    public ItemMetaData GetMetaDataWithID(int id)
    {
        return _metaData.Find(data => data.ID == id);
    }
}
