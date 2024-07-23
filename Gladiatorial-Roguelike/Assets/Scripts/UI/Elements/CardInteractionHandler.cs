using System;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class CardInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;
        public event Action<CardView> OnCardClick;
        public event Action<CardView, PointerEventData> OnBeginDragAction;
        public event Action<CardView, PointerEventData> OnDragAction;
        public event Action<CardView, PointerEventData> OnEndDragAction;

        private CardView _cardView;

        public void Initialize(CardView cardView) => _cardView = cardView;

        public void OnPointerEnter(PointerEventData eventData) => OnCardHoverEnter?.Invoke(_cardView);

        public void OnPointerExit(PointerEventData eventData) => OnCardHoverExit?.Invoke(_cardView);

        public void OnPointerClick(PointerEventData eventData) => OnCardClick?.Invoke(_cardView);

        public void OnBeginDrag(PointerEventData eventData) => OnBeginDragAction?.Invoke(_cardView, eventData);

        public void OnDrag(PointerEventData eventData) => OnDragAction?.Invoke(_cardView, eventData);

        public void OnEndDrag(PointerEventData eventData) => OnEndDragAction?.Invoke(_cardView, eventData);
    }
}