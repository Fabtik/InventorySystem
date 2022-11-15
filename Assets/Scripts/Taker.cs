using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenTake = 1f;

    private UIInventory _inventory;
    private MetaDataStorage _metaDataStorage;


    private bool _isTakeResource = true;

    private void Awake()
    {
        _inventory = FindObjectOfType<UIInventory>();
        _metaDataStorage = FindObjectOfType<MetaDataStorage>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_isTakeResource)
            {
                _isTakeResource = false;
                StartCoroutine(TakeRandomResource());
            }
        }

    }


    private IEnumerator TakeRandomResource()
    {
        RemoveWood();
        RemoveStone();

        yield return new WaitForSeconds(_timeBetweenTake);
        _isTakeResource = true;
    }

    private void RemoveWood()
    {
        var randCount = Random.Range(1, 4);
        _inventory._inventory.RemoveItemWithID(this, 0, randCount);
    }

    private void RemoveStone()
    {
        var randCount = Random.Range(1, 4);
        _inventory._inventory.RemoveItemWithID(this, 1, randCount);
    }

}
