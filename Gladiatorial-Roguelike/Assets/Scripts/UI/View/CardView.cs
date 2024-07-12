using Logic.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using System;

namespace UI.Elements
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardDragHandler _cardDragHandler;
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;

        private CardData _cardData;
        
        public void Initialize(CardData cardData)
        {
            _cardData = cardData;
        }

        public CardData GetCardData() => _cardData;
        public CardDragHandler GetCardDragHandler() => _cardDragHandler;
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnCardHoverEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnCardHoverExit?.Invoke(this);
        }
    }
}