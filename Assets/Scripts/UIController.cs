
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _UIInventory;
   

    private void Start()
    {
        _UIInventory.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _UIInventory.SetActive(!_UIInventory.activeSelf);
        }
    }
}
