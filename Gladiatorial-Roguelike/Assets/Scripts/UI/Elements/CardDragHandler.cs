using UI.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Elements
{
    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CardView _cardView;
        private CardDragService _cardDragService;
        
        private bool _isDraggable;

        [Inject]
        private void Inject(CardDragService cardDragService) => 
            _cardDragService = cardDragService;

        public void Init(CardView cardView, bool isDraggable)
        {
            _isDraggable = isDraggable;
            _cardView = cardView;
        }

        public void OnBeginDrag(PointerEventData eventData) => 
            _cardDragService.HandleBeginDrag(_cardView, eventData, _isDraggable);

        public void OnEndDrag(PointerEventData eventData) => 
            _cardDragService.HandleEndDrag(eventData, _isDraggable);

        public void OnDrag(PointerEventData eventData) => 
            _cardDragService.HandleDrag(eventData);
    }
}