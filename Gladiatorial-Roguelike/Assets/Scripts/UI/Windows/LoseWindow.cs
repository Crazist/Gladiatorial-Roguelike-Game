using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Infrastructure.States;
using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class LoseWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private RectTransform _deckViewContent;
        [SerializeField] private CardView _cardPrefab;

        private PlayerDeckService _playerDeckService;
        private PermaDeckService _permaDeckService;
        private GameStateMachine _gameStateMachine;
        private SaveLoadService _saveLoadService;
        private bool _cardSelected = false;
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(PlayerDeckService playerDeckService, PermaDeckService permaDeckService,
            GameStateMachine gameStateMachine, SaveLoadService saveLoadService,
            PersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _permaDeckService = permaDeckService;
            _playerDeckService = playerDeckService;

            InitializeBtns();
            SpawnCards();
        }

        private void InitializeBtns() =>
            _continueButton.onClick.AddListener(OnContinueButtonClicked);

        private void SpawnCards()
        {
            foreach (var cardData in _playerDeckService.GetDeck())
            {
                CardView cardView = Instantiate(_cardPrefab, _deckViewContent);
                cardView.Initialize(cardData, TeamType.None, false);
                cardView.GetCardInteractionHandler().OnCardClick += OnCardClick;
            }
        }

        private void OnCardClick(CardView cardView)
        {
            if (_cardSelected)
                return;

            _cardSelected = true;
            _permaDeckService.AddCardToDeck(cardView.GetCard());
        }

        private void OnContinueButtonClicked()
        {
            _persistentProgressService.PlayerProgress.CurrentRun.RefreshCurrentRun();
            _saveLoadService.SaveProgress();
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}