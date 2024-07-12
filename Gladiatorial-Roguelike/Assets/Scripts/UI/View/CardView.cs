using System;
using Logic.Cards;
using Logic.Enteties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _cardImage;
        [SerializeField] private TMP_Text _cardHp;

        private CardData _cardData;

        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;

        public void Initialize(CardData cardData)
        {
            _cardData = cardData;
            _cardImage.sprite = cardData.Icon;
            _cardHp.text = $"HP: {cardData.Hp}";
        }

        public CardData GetCardData()
        {
            return _cardData;
        }

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