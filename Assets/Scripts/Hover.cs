using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>(); 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.outlineWidth = .2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.outlineWidth = 0;
    }

    private void OnDestroy()
    {
        _text.outlineWidth = 0;
    }
}
