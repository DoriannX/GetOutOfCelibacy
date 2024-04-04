using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toggler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool IsSelected;
    [SerializeField] Sprite _selectedSprite;
    Color _defaultColor;
    [SerializeField] Color _hoveredColor;
    Sprite _defaultSprite;

    Image _itemImage;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
    }

    private void Start()
    {
        _defaultSprite = _itemImage.sprite;
        _defaultColor = _itemImage.color;
    }

    public void SwitchSelection()
    {
        print("Selected / Unselected");
        IsSelected = (IsSelected) ? false : true;
        if (IsSelected)
            _itemImage.sprite = _selectedSprite;
        else
            _itemImage.sprite = _defaultSprite;
        _itemImage.SetNativeSize();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemImage.color = _hoveredColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemImage.color = _defaultColor;
    }

    private void OnMouseDown()
    {
        print("selected");
    }
}
