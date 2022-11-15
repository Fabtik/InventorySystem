using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _UIInventory;

    [SerializeField] private Text _textName;
    [SerializeField] private Text _textDescription;

    private void Start()
    {
        _UIInventory.SetActive(false);
        _textName.gameObject.SetActive(false);
        _textDescription.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            var state = !_UIInventory.activeSelf;
            _UIInventory.SetActive(state);

            _textName.text = "";
            _textDescription.text = "";

            _textName.gameObject.SetActive(state);
            _textDescription.gameObject.SetActive(state);
        }
    }
}
