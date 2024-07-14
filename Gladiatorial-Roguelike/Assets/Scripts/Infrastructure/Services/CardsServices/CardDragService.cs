using UI.Elements;
using UI.Factory;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Services
{
    public class CardDragService
    {
        public bool IsDrag { get; private set; }
        public Action OnCardBeginDrag { get; set; }
        public Action OnCardEndDrag { get; set; }

        private UIFactory _uiFactory;
        private RectTransform _sellArea; 
        private CardSellService _cardSellService;
        private CardView _currentCardView;
       
        private Vector3 _startPosition;
        private Vector2 _offset;

        [Inject]
        private void Inject(CardSellService cardSellService, UIFactory uiFactory)
        {
            _cardSellService = cardSellService;
            _uiFactory = uiFactory;
        }

        public void SetSellArea(RectTransform sellArea) =>
            _sellArea = sellArea;

        public void HandleBeginDrag(CardView cardView, PointerEventData eventData, bool isDraggable)
        {
            if (!isDraggable) return;

            _currentCardView = cardView;
            IsDrag = true;

            InitializeDrag(eventData);
            OnCardBeginDrag?.Invoke();
        }

        public void HandleDrag(PointerEventData eventData)
        {
            if (IsDrag) 
                PerformDrag(eventData);
        }

        public void HandleEndDrag(PointerEventData eventData, bool isDraggable)
        {
            if (!isDraggable || !IsDrag) return;

            EndDrag(eventData);
            OnCardEndDrag?.Invoke();

            ResetDrag();
        }

        private void InitializeDrag(PointerEventData eventData)
        {
            _startPosition = _currentCardView.transform.localPosition;

            var localPoint = LocalPoint(eventData);

            _offset = _startPosition - (Vector3)localPoint;
        }

        private void PerformDrag(PointerEventData eventData)
        {
            var localPoint = LocalPoint(eventData);

            _currentCardView.transform.localPosition = localPoint + _offset;
        }

        private void EndDrag(PointerEventData eventData)
        {
            if (IsInSellArea(eventData))
            {
                _cardSellService.SellCard(_currentCardView);
            }
            else
            {
                ResetPosition(_currentCardView);
            }
        }

        private bool IsInSellArea(PointerEventData eventData)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_sellArea, eventData.position,
                eventData.pressEventCamera);
        }

        private Vector2 LocalPoint(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _uiFactory.UI.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint);
            return localPoint;
        }

        private void ResetDrag()
        {
            IsDrag = false;
            _currentCardView = null;
        }

        public void ResetPosition(CardView cardView) =>
            cardView.transform.localPosition = _startPosition;
    }
}
