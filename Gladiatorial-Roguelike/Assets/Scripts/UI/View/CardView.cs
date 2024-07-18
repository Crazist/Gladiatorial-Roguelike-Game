using Logic.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Infrastructure.Services;
using Logic.Entities;
using Zenject;

namespace UI.Elements
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private DynamicCardView _dynamicCard;
        [SerializeField] private CardDragHandler _cardDragHandler;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CardDisplay _cardDisplay;
        [SerializeField] private CanvasGroup _canvasGroup;
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;

        private Card _card;
        private TableService _tableService;

        [Inject]
        private void Inject(TableService tableService) =>
            _tableService = tableService;

        public void Initialize(Card card, bool isDraggable)
        {
            _card = card;
            _cardDragHandler.Init(this, isDraggable);
            card.InitializeView(_dynamicCard);
            _cardDisplay.Initialize(card);
        }

        public void UpdateView()
        {
            _card.InitializeView(_dynamicCard);
            _cardDisplay.Initialize(_card);
        }

        public Card GetCard() =>
            _card;

        public RectTransform GetRectTransform() =>
            _rectTransform;

        public CardDisplay GetCardDisplay() =>
            _cardDisplay;

        public CardDragHandler GetCardDragHandler() =>
            _cardDragHandler;

        public void ChangeRaycasts(bool on) => 
            _canvasGroup.blocksRaycasts = on;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_cardDisplay.IsFaceUp())
            {
                _tableService.SetHoveredCard(this);
                OnCardHoverEnter?.Invoke(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_cardDisplay.IsFaceUp())
            {
                OnCardHoverExit?.Invoke(this);
                _tableService.SetHoveredCard(null);
            }
        }
    }
}
