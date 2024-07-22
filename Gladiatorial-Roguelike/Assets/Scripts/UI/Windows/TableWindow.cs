using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class TableWindow : WindowBase
    {
        [SerializeField] private List<CardDropArea> _playerDropAreas;
        [SerializeField] private List<EnemyCardDropArea> _enemyDropAreas;

        [SerializeField] private Image _playerDeck;
        [SerializeField] private Image _enemyDeck;
        [SerializeField] private Transform _playerHandArea;
        [SerializeField] private Transform _enemyHandArea;
        [SerializeField] private CardView _cardPrefab;

        private TableService _tableService;
        private StaticDataService _staticDataService;
        private PersistentProgressService _persistentProgressService;
        private CardPopupService _cardPopup;
        private CardDragService _cardDragService;
        private AIService _aiService;

        [Inject]
        private void Inject(TableService tableService, StaticDataService staticDataService,
            PersistentProgressService persistentProgressService, CardPopupService cardPopup,
            CardDragService cardDragService,
            AIService aiService)
        {
            _cardDragService = cardDragService;
            _cardPopup = cardPopup;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _tableService = tableService;
            _aiService = aiService;

            InitializePlayerHand();
            InitializeEnemyHand();
            SetDeckImages();
            InitializeDropAreas();
        }

        private void SetDeckImages()
        {
            _playerDeck.sprite = _staticDataService
                .ForDeck(_persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.CurrentDeck).CardBackImage;
            _enemyDeck.sprite = _staticDataService
                .ForDeck(_persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.EnemyDeckType)
                .CardBackImage;
        }

        public void InitializePlayerHand()
        {
            foreach (var card in _tableService.DrawPlayerHand())
            {
                var cardView = Instantiate(_cardPrefab, _playerHandArea);
                cardView.Initialize(card, true);
                _cardPopup.SubscribeToCard(cardView);
                _tableService.GetPlayerHandViews().Add(cardView);
            }
        }

        public void InitializeEnemyHand()
        {
            foreach (var card in _tableService.DrawEnemyHand())
            {
                var cardView = Instantiate(_cardPrefab, _enemyHandArea);
                cardView.Initialize(card, false);
                cardView.GetCardDisplay().SetFaceDown();
                _cardPopup.SubscribeToCard(cardView);
                _tableService.GetEnemyHandViews().Add(cardView);
            }
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