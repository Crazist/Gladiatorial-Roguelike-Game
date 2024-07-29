using Infrastructure.Services;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Services.CardsServices;
using Infrastructure.StateMachines;
using Infrastructure.States;
using Logic.Types;
using UI.Elements.CardDrops;
using UI.Service;
using UI.Type;
using Zenject;

namespace UI.Windows
{
    public class BlacksmithWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private RectTransform _deckViewContent;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private Button _deckButton;
        
        [SerializeField] private HealCardDropArea _healArea;
        [SerializeField] private UpgradeCardDropArea _upgradeArea;
        [SerializeField] private SellArea _saleArea;

        private CardDragService _cardDragService;
        private PlayerDeckService _playerDeckService;
        private GameStateMachine _gameStateMachine;
        private WindowService _windowService;

        private bool _cardsInitialized = false;

        [Inject]
        private void Inject(PlayerDeckService playerDeckService, CardDragService cardDragService,
            GameStateMachine gameStateMachine, WindowService windowService)
        {
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
            _playerDeckService = playerDeckService;
            _cardDragService = cardDragService;

            InitializeBtns();
            InitializeDropAreas();
        }

        private void InitializeBtns()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _deckButton.onClick.AddListener(OnDeckButtonClicked);
        }

        private void InitializeDropAreas()
        {
            _cardDragService.AddDropArea(_healArea);
            _cardDragService.AddDropArea(_upgradeArea);
            _cardDragService.AddDropArea(_saleArea);
        }

        private void OnContinueButtonClicked()
        {
            _gameStateMachine.Enter<MenuState>();
            _windowService.Open(WindowId.EnemyChoose);
        }

        private void OnDeckButtonClicked()
        {
            _deckViewContent.gameObject.SetActive(!_deckViewContent.gameObject.activeSelf);

            if (!_cardsInitialized)
            {
                InitializeCards();
                _cardsInitialized = true;
            }
        }

        private void InitializeCards()
        {
            foreach (var cardData in _playerDeckService.GetDeck())
            {
                CardView cardView = Instantiate(_cardPrefab, _deckViewContent);
                cardView.Initialize(cardData, TeamType.None, true);
            }
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _deckButton.onClick.RemoveListener(OnDeckButtonClicked);
        }
    }
}
