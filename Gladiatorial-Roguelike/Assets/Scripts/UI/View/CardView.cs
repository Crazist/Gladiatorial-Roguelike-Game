using System;
using Infrastructure.Services;
using Infrastructure.Services.BattleServices;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UI.Factory;
using UI.Services;
using UnityEngine;
using Zenject;

namespace UI.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private DynamicCardView _dynamicCard;
        [SerializeField] private CardDragHandler _cardDragHandler;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CardDisplay _cardDisplay;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private CardInteractionHandler _interactionHandler;
        [SerializeField] private AttackAndDefence _attackAndDefence;
        public event Action<CardView> OnCardHoverEnter;
        public event Action<CardView> OnCardHoverExit;
        public CardState State { get; set; }
        public TeamType Team { get; set; }

        private Card _card;
        private TableService _tableService;
        private TurnService _turnService;
        private AttackService _attackService;
        private CardDragService _cardDragService;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(TableService tableService, TurnService turnService, AttackService attackService,
            CardDragService cardDragService)
        {
            _cardDragService = cardDragService;
            _attackService = attackService;
            _tableService = tableService;
            _turnService = turnService;
        }

        public void Initialize(Card card, TeamType team, bool isDraggable)
        {
            _card = card;
            Team = team;

            _card.InitializeView(_dynamicCard);
            _interactionHandler.Initialize(this);
            _cardDragHandler.Init(this, _cardDragService, _interactionHandler, isDraggable);
            _cardDisplay.Initialize(card);
            _attackAndDefence.Initialize(this, _interactionHandler);

            _interactionHandler.OnCardHoverEnter += HandleCardHoverEnter;
            _interactionHandler.OnCardHoverExit += HandleCardHoverExit;

            _turnService.OnPlayerTurnStart += EnableInteraction;
            _turnService.OnEnemyTurnStart += DisableInteraction;
        }

        public void UpdateView()
        {
            _card.InitializeView(_dynamicCard);
            _cardDisplay.Initialize(_card);
        }

        public void ChangeRaycasts(bool on) => _canvasGroup.blocksRaycasts = on;

        public Card GetCard() => _card;

        public RectTransform GetRectTransform() => _rectTransform;

        public CardDisplay GetCardDisplay() => _cardDisplay;

        public CardDragHandler GetCardDragHandler() => _cardDragHandler;

        private void HandleCardHoverEnter(CardView cardView)
        {
            if (_cardDisplay.IsFaceUp())
            {
                _tableService.SetHoveredCard(this);
                OnCardHoverEnter?.Invoke(this);
            }
        }

        private void HandleCardHoverExit(CardView cardView)
        {
            if (_cardDisplay.IsFaceUp())
            {
                OnCardHoverExit?.Invoke(this);
                _tableService.SetHoveredCard(null);
            }
        }

        private void OnDestroy()
        {
            _turnService.OnPlayerTurnStart -= EnableInteraction;
            _turnService.OnEnemyTurnStart -= DisableInteraction;
            _interactionHandler.OnCardHoverEnter -= HandleCardHoverEnter;
            _interactionHandler.OnCardHoverExit -= HandleCardHoverExit;
        }

        private void EnableInteraction() => ChangeRaycasts(true);

        private void DisableInteraction() => ChangeRaycasts(false);
    }
}