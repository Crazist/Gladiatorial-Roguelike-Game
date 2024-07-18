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
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CardDisplay _cardDisplay;
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;

        private Card _card;

        public void Initialize(Card card, bool isDraggable)
        {
            _card = card;
            _cardDragHandler.Init(this, isDraggable);
          
            card.InitializeView(_dynamicCard);
            _cardDisplay.Initialize(card);
        }
        
        public Card GetCard() =>
            _card;
        public RectTransform GetRectTransform() => 
            _rectTransform;
        public CardDisplay GetCardDisplay() =>
            _cardDisplay;
        public CardDragHandler GetCardDragHandler() =>
            _cardDragHandler;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_cardDisplay.IsFaceUp()) OnCardHoverEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_cardDisplay.IsFaceUp()) OnCardHoverExit?.Invoke(this);
        }
    }
}