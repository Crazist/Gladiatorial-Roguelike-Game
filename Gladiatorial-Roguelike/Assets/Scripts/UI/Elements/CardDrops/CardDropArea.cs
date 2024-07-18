using UI.Elements;
using UI.Services;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardDropArea : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    public abstract void HandleDrop(CardView cardView, CardDragService cardDragService);

    public bool IsInDropArea(PointerEventData eventData) => 
        RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, eventData.position, eventData.pressEventCamera);
}