using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giver : MonoBehaviour
{
    [SerializeField] private float _timeBetweenGifts = 1f;

    private UIInventory _inventory;
    private MetaDataStorage _metaDataStorage;  

    private bool _isGetRevard = true;

    private void Awake()
    {
        _inventory = FindObjectOfType<UIInventory>();
        _metaDataStorage = FindObjectOfType<MetaDataStorage>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (_isGetRevard)
            {
                _isGetRevard = false;
                StartCoroutine(GetRandomRevard());
            }
        }  
        
    }
    private IEnumerator GetRandomRevard()
    {
        var randCount = Random.Range(1, 10);
        var wood = new Item(_metaDataStorage.GetMetaDataWithID(0));
        wood.state.amount = randCount;
        _inventory._inventory.TryToAdd(this, wood);


        randCount = Random.Range(1, 5);
        var stone = new Item(_metaDataStorage.GetMetaDataWithID(1));
        stone.state.amount = randCount;
        _inventory._inventory.TryToAdd(this, stone);

        var apple = new Food(_metaDataStorage.GetMetaDataWithID(2));
        apple.state.amount = 1;
        _inventory._inventory.TryToAdd(this, apple);


        yield return new WaitForSeconds(_timeBetweenGifts);
        _isGetRevard = true;
    }

    
}
