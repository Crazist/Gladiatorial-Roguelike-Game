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
        private CanvasService _canvasService;

        private Vector3 _startPosition;
        private Vector2 _offset;
        private CardBuffService _cardBuffService;

        [Inject]
        private void Inject(UIFactory uiFactory, CardBuffService cardBuffService, CanvasService canvasService)
        {
            _cardBuffService = cardBuffService;
            _uiFactory = uiFactory;
            _canvasService = canvasService;

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
            _canvasService.MoveToOverlay(cardView.GetRectTransform());
            AdjustPositionToCanvas(cardView.GetRectTransform(), eventData);
            OnCardBeginDrag?.Invoke();
        }

        public void ResetPosition(CardView cardView)
        {
            cardView.transform.localPosition = _startPosition;
            _canvasService.MoveBack(cardView.GetRectTransform());
        }

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

        private void ApplyBuff() => 
            _cardBuffService.ApplyBuff(_currentCardView);

        private void HandleSuccessfulDrop(CardDropArea dropArea)
        {
            dropArea.HandleDrop(_currentCardView, this);
            OnCardEndDrag?.Invoke();
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
                _canvasService.GetOverlayCanvas().transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint);
            return localPoint;
        }

        private void ResetDrag()
        {
            IsDrag = false;
            _canvasService.MoveBack(_currentCardView.GetRectTransform());
            _currentCardView = null;
        }

        private void AdjustPositionToCanvas(RectTransform rectTransform, PointerEventData eventData)
        {
            var worldPoint = Vector3.zero;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
                    eventData.pressEventCamera, out worldPoint))
            {
                rectTransform.position = worldPoint;
            }

            var localPoint = LocalPoint(eventData);
            _offset = rectTransform.localPosition - (Vector3)localPoint;
        }
    }
}