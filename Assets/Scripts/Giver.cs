using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giver : MonoBehaviour
{
    [SerializeField] private float _timeBetweenGifts = 1f;

    private Inventory _inventory;
    private MetaDataStorage _metaDataStorage;

    

    private bool _isGetRevard = true;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
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
        var allSlots = _inventory._inventory.GetAllSlots();
        var availableSlots = new List<ISlot>(allSlots);

        var filledSlots = 2;
        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomWoodIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomStoneIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }



        yield return new WaitForSeconds(_timeBetweenGifts);
        _isGetRevard = true;
    }

    private ISlot AddRandomWoodIntoRandomSlot(List<ISlot> slots)
    {
        var randSlotIndex = Random.Range(0, slots.Count);
        var randSlot = slots[randSlotIndex];
        var randCount = Random.Range(1, 3);
        var wood = new Item(_metaDataStorage.GetMetaDataWithID(0));
        wood.state.amount = randCount;
        _inventory._inventory.TryToAddToSlot(this, randSlot, wood);
        return randSlot;
    }

    private ISlot AddRandomStoneIntoRandomSlot(List<ISlot> slots)
    {
        var randSlotIndex = Random.Range(0, slots.Count);
        var randSlot = slots[randSlotIndex];
        var randCount = Random.Range(1, 3);
        var stone = new Item(_metaDataStorage.GetMetaDataWithID(1));
        stone.state.amount = randCount;
        _inventory._inventory.TryToAddToSlot(this, randSlot, stone);
        return randSlot;
    }

}
