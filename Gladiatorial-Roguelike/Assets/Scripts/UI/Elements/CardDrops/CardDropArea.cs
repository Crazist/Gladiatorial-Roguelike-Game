using UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardDropArea : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public abstract void HandleDrop(CardView cardView);

    public bool IsInDropArea(PointerEventData eventData)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, eventData.position, eventData.pressEventCamera);
    }
}