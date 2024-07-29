using System;
using System.Collections.Generic;
using Logic.Types;
using UI.Elements.CardDrops;
using UI.Factory;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class CardDragService
    {
        public bool IsDrag { get; private set; }
        public Action OnCardBeginDrag { get; set; }
        public Action OnCardEndDrag { get; set; }

        private UIFactory _uiFactory;
        private List<CardDropArea> _dropAreas;
        private CardView _currentCardView;
       
        private Vector3 _startPosition;
        private Vector2 _offset;
        private CardBuffService _cardBuffService;
        
        [Inject]
        private void Inject(UIFactory uiFactory,  CardBuffService cardBuffService)
        {
            _cardBuffService = cardBuffService;
            _uiFactory = uiFactory;
            
            _dropAreas = new List<CardDropArea>();
        }

        public void AddDropArea(CardDropArea dropArea)
        {
            if (!_dropAreas.Contains(dropArea))
            {
                _dropAreas.Add(dropArea);
            }
        }

        public void CleanUp() => _dropAreas.Clear();

        public void HandleBeginDrag(CardView cardView, PointerEventData eventData, bool isDraggable)
        {
            if (!isDraggable) return;

            _currentCardView = cardView;
            IsDrag = true;

            InitializeDrag(eventData);
            OnCardBeginDrag?.Invoke();
        }

        public void ResetPosition(CardView cardView) =>
            cardView.transform.localPosition = _startPosition;

        private void InitializeDrag(PointerEventData eventData)
        {
            _startPosition = _currentCardView.transform.localPosition;

            var localPoint = LocalPoint(eventData);

            _offset = _startPosition - (Vector3)localPoint;
        }

        public void HandleDrag(PointerEventData eventData, bool isDraggable)
        {
            if (IsDrag && isDraggable) 
                PerformDrag(eventData);
        }

        public void HandleEndDrag(PointerEventData eventData, bool isDraggable)
        {
            if (!isDraggable || !IsDrag)
            {
                ResetDrag();
                return;
            }

            if (_currentCardView.GetCard().CardData.Category == CardCategory.Special)
            {
                ApplyBuff();
                return;
            }

            foreach (var dropArea in _dropAreas)
            {
                if (dropArea.IsInDropArea(eventData) && !dropArea.IsOccupied())
                {
                    HandleSuccessfulDrop(dropArea);
                    return;
                }
            }

            HandleUnsuccessfulDrop();
        }

        private void ApplyBuff()
        {
            var currentCardViewCopy = _currentCardView;
            
            _cardBuffService.ApplyBuff(_currentCardView, () => ResetPosition(currentCardViewCopy));
            
            ResetDrag();
        }

        private void HandleSuccessfulDrop(CardDropArea dropArea)
        {
            dropArea.HandleDrop(_currentCardView, this);
            OnCardEndDrag?.Invoke();
            ResetDrag();
        }

        private void HandleUnsuccessfulDrop()
        {
            ResetPosition(_currentCardView);
            OnCardEndDrag?.Invoke();
            ResetDrag();
        }

        private void PerformDrag(PointerEventData eventData)
        {
            var localPoint = LocalPoint(eventData);

            _currentCardView.transform.localPosition = localPoint + _offset;
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
    }
}
