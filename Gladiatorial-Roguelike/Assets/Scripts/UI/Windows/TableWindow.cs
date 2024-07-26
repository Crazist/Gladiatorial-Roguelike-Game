using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.AIServices;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Infrastructure.States.BattleStates;
using Logic.Types;
using UI.Elements;
using UI.Elements.CardDrops;
using UI.Services;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class TableWindow : WindowBase
    {
        [SerializeField] private List<CardDropArea> _playerDropAreas;
        [SerializeField] private List<EnemyCardDropArea> _enemyDropAreas;

        [SerializeField] private DeckBattleView _playerDeckView;
        [SerializeField] private DeckBattleView _enemyDeckView;
        [SerializeField] private Transform _playerHandArea;
        [SerializeField] private Transform _enemyHandArea;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private Button _finishTurn;

        private TableService _tableService;
        private StaticDataService _staticDataService;
        private PersistentProgressService _persistentProgressService;
        private CardPopupService _cardPopup;
        private CardDragService _cardDragService;
        private AIService _aiService;
        private BattleStateMachine _battleStateMachine;
        private TurnService _turnService;
        private CardService _cardService;

        [Inject]
        private void Inject(TableService tableService, StaticDataService staticDataService,
            PersistentProgressService persistentProgressService, CardPopupService cardPopup,
            CardDragService cardDragService, AIService aiService, BattleStateMachine battleStateMachine,
            TurnService turnService, CardService cardService)
        {
            _turnService = turnService;
            _battleStateMachine = battleStateMachine;
            _cardDragService = cardDragService;
            _cardPopup = cardPopup;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _tableService = tableService;
            _aiService = aiService;
            _cardService = cardService;

            SubscribeToTurnEvents();
            InitializeBtns();
            InitializePlayerHand();
            InitializeEnemyHand();
            SetDeckViews();
            InitializeDropAreas();
        }

        protected override void OnAwake() => _finishTurn.gameObject.SetActive(false);

        private void SubscribeToTurnEvents()
        {
            _turnService.OnPlayerTurnStart += OnPlayerTurnStart;
            _turnService.OnTurnEnd += NewTurn;
        }

        private void OnPlayerTurnStart() => _finishTurn.gameObject.SetActive(true);

        private void InitializeBtns() => _finishTurn.onClick.AddListener(OnFinishTurnClicked);

        private void OnFinishTurnClicked()
        {
            _finishTurn.gameObject.SetActive(false);
            _battleStateMachine.Enter<BattleCalculationState>();
        }

        private void SetDeckViews()
        {
            var playerDeck = _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.CurrentDeck;
            var enemyDeck = _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.EnemyDeckType;

            _playerDeckView.SetDeckImage(_staticDataService.ForDeck(playerDeck).CardBackImage);
            _enemyDeckView.SetDeckImage(_staticDataService.ForDeck(enemyDeck).CardBackImage);

            UpdateDeckCounts();
        }

        private void UpdateDeckCounts()
        {
            _playerDeckView.UpdateDeckCount(_cardService.GetRemainingPlayerCardsCount(), _cardService.GetMaxPlayerCardsCount());
            _enemyDeckView.UpdateDeckCount(_cardService.GetRemainingEnemyCardsCount(), _cardService.GetMaxEnemyCardsCount());
        }

        private void InitializePlayerHand()
        {
            foreach (var card in _tableService.DrawPlayerHand())
            {
                var cardView = Instantiate(_cardPrefab, _playerHandArea);
                cardView.Initialize(card, TeamType.Ally, true);
                cardView.State = CardState.OnHand;
                _cardPopup.SubscribeToCard(cardView);
                _tableService.GetPlayerHandViews().Add(cardView);
            }
        }

        private void InitializeEnemyHand()
        {
            foreach (var card in _tableService.DrawEnemyHand())
            {
                var cardView = Instantiate(_cardPrefab, _enemyHandArea);
                cardView.Initialize(card, TeamType.Enemy, false);
                cardView.State = CardState.OnHand;
                cardView.GetCardDisplay().SetFaceDown();
                _cardPopup.SubscribeToCard(cardView);
                _tableService.GetEnemyHandViews().Add(cardView);
            }
        }

        private void NewTurn()
        {
            InitializePlayerHand();
            InitializeEnemyHand();
            
            UpdateDeckCounts();
        }

        private void InitializeDropAreas()
        {
            foreach (var dropArea in _playerDropAreas)
            {
                _cardDragService.AddDropArea(dropArea);
            }

            _aiService.Initialize(_enemyDropAreas);
        }
    }
}
