using UI.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UI.View;

namespace UI.Elements
{
    public class CardDragHandler : MonoBehaviour
    {
        private CardView _cardView;
        private CardDragService _cardDragService;
        private CardInteractionHandler _cardInteractionHandler;

        private bool _isDraggable;

        public void Init(CardView cardView, CardDragService cardDragService,
            CardInteractionHandler cardInteractionHandler, bool isDraggable)
        {
            _cardInteractionHandler = cardInteractionHandler;
            _cardDragService = cardDragService;
            _isDraggable = isDraggable;
            _cardView = cardView;

            _cardInteractionHandler.OnBeginDragAction += HandleBeginDrag;
            _cardInteractionHandler.OnDragAction += HandleDrag;
            _cardInteractionHandler.OnEndDragAction += HandleEndDrag;
        }

        public void ChangeDraggable(bool isDraggable) => _isDraggable = isDraggable;

        private void HandleBeginDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDraggable)
            {
                _cardDragService.HandleBeginDrag(_cardView, eventData, _isDraggable);
            }
        }

        private void HandleDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDraggable)
            {
                _cardDragService.HandleDrag(eventData, _isDraggable);
            }
        }

        private void HandleEndDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDraggable)
            {
                _cardDragService.HandleEndDrag(eventData, _isDraggable);
            }
        }
    }
}