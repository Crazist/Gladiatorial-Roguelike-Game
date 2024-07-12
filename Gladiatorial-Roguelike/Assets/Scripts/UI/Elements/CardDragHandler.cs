using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<CardView, PointerEventData> OnCardBeginDrag;
        public event Action<CardView, PointerEventData> OnCardDrag;
        public event Action<CardView, PointerEventData> OnCardEndDrag;

        private CardView _cardView;
        private Canvas _canvas;
        private Vector3 _startPosition;
        private Vector2 _offset;

        public void Init(CardView cardView, Canvas canvas)
        {
            _cardView = cardView;
            _canvas = canvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(_canvas == null) return;
            
            _startPosition = transform.localPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform, 
                eventData.position, 
                eventData.pressEventCamera, 
                out Vector2 localPoint);

            _offset = transform.localPosition - (Vector3)localPoint;
            OnCardBeginDrag?.Invoke(_cardView, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canvas == null)
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform, 
                eventData.position, 
                eventData.pressEventCamera, 
                out Vector2 localPoint);

            transform.localPosition = localPoint + _offset;
            OnCardDrag?.Invoke(_cardView, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnCardEndDrag?.Invoke(_cardView, eventData);
        }

        public void ResetPosition()
        {
            transform.localPosition = _startPosition;
        }
    }
}