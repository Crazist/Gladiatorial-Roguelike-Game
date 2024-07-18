using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class TableWindow: WindowBase
    {
        [SerializeField] private Image _playerDeck;
        [SerializeField] private Image _enemyDeck;
        [SerializeField] private Transform _playerHandArea;
        [SerializeField] private Transform _enemyHandArea;
        [SerializeField] private CardView _cardPrefab;

        private CardService _cardService;
        private TableService _tableService;
        private StaticDataService _staticDataService;
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(CardService cardService, TableService tableService, StaticDataService staticDataService,
            PersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _tableService = tableService;
            _cardService = cardService;

            InitializePlayerHand();
            InitializeEnemyHand();
            SetDeckImages();
        }

        private void SetDeckImages()
        {
            _playerDeck.sprite = _staticDataService
                .ForDeck(_persistentProgressService.PlayerProgress.DeckProgress.CurrentDeck).CardBackImage;
            _enemyDeck.sprite = _staticDataService
                .ForDeck(_persistentProgressService.PlayerProgress.EnemyProgress.EnemyDeckType).CardBackImage;
        }

        public void InitializePlayerHand()
        {
            foreach (var card in _tableService.GetPlayerHand())
            {
                var cardView = Instantiate(_cardPrefab, _playerHandArea);
                cardView.Initialize(card, true);
            }
        }

        public void InitializeEnemyHand()
        {
            foreach (var card in _tableService.GetEnemyHand())
            {
                var cardView = Instantiate(_cardPrefab, _enemyHandArea);
                cardView.Initialize(card, false);
            }
        }
    }
}