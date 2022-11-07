using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;

    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public IItem _item { get; private set; }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor ;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Refresh(ISlot slot)
    {
        if (slot.isEmpty)
        {
            _text.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
            return;
        }

        _item = slot.item;
        _image.sprite = _item.data.sprite;
        _image.gameObject.SetActive(true);

        var isTextAmountEnabled = slot.amount > 1;//
        _text.gameObject.SetActive(isTextAmountEnabled);   //

        if (isTextAmountEnabled)
        {
            _text.text = slot.amount.ToString();
        }



    }
}
