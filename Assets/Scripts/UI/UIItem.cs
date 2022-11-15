using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    private UISlot _UISlot;

    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _nameText;

    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public IItem _item { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _UISlot = GetComponentInParent<UISlot>();
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionText.text = _item.data.description;
        _nameText.text = _item.data.Name;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionText.text = "";
        _nameText.text = "";
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {

        if (_item.GetType().BaseType == typeof(UsableItem))
        {
            Use((UsableItem)_item);
        }
       
    }

    public void Use<T>(T item) where T : UsableItem
    {
        item.Use();
        _UISlot.slot.Clear();
        _UISlot.Refresh();
    }

}
