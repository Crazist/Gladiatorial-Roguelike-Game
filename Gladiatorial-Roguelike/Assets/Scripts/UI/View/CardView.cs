using Logic.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Logic.Entities;

namespace UI.Elements
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private DynamicCardView _dynamicCard;
        [SerializeField] private CardDragHandler _cardDragHandler;
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;

        private Card _card;

        public void Initialize(Card card, bool isDraggable)
        {
            _card = card;
            _cardDragHandler.Init(this, isDraggable);
            card.InitializeView(_dynamicCard);
        }
        
        public Card GetCard() =>
            _card;
        public DynamicCardView GetDynamicCardView() =>
            _dynamicCard;
        public void OnPointerEnter(PointerEventData eventData) =>
            OnCardHoverEnter?.Invoke(this);

        public void OnPointerExit(PointerEventData eventData) =>
            OnCardHoverExit?.Invoke(this);
    }
}