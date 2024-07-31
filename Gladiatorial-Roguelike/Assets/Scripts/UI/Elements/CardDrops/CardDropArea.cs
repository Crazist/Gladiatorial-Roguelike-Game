using Infrastructure.Services.CardsServices;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements.CardDrops
{
    public abstract class CardDropArea : MonoBehaviour
    {
        [SerializeField] protected RectTransform _rectTransform;
        
        protected CardView OccupiedCard;
        public abstract void HandleDrop(CardView cardView, CardDragService cardDragService);
        public bool IsInDropArea(PointerEventData eventData) => 
            RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, eventData.position, eventData.pressEventCamera);
        public bool IsOccupied() => 
            OccupiedCard != null;
    }
}