using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject _inventory;
   

    private void Awake()
    {
        _inventory = GetComponentInChildren<Test>().gameObject;
        _inventory.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {      
            _inventory.SetActive(!_inventory.activeSelf);
        }

    }
}
