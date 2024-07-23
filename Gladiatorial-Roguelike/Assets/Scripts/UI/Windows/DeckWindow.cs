using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UI.Model;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DeckWindow : WindowBase
    {
        [SerializeField] private Image _deckImage;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private Transform _cardGroup;
        [SerializeField] private Button _continueBtn;

        private DeckType _deckType;

        private StaticDataService _staticDataService;
        private CardPopupService _cardPopupService;
        private PersistentProgressService _persistentProgressService;
        private PermaDeckModel _permaDeckModel;
        private SaveLoadService _saveLoadService;
        private PlayerDeckService _playerDeckService;
        private Infrastructure.Factory _factory;

        [Inject]
        private void Inject(DeckViewModel deckViewModel, StaticDataService staticDataService,
            CardPopupService cardPopupService, PersistentProgressService persistentProgressService, 
            PermaDeckModel permaDeckModel, SaveLoadService saveLoadService, PlayerDeckService playerDeckService,
            Infrastructure.Factory factory)
        {
            _factory = factory;
            _playerDeckService = playerDeckService;
            _saveLoadService = saveLoadService;
            _permaDeckModel = permaDeckModel;
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;
            _staticDataService = staticDataService;

            Initialize(deckViewModel);
        }

        private void Initialize(DeckViewModel deckViewModel)
        {
            SetDeckType(deckViewModel);
            InitializeDeck(deckViewModel);
            RegisterButtons();
            SetupContinueButton(deckViewModel.HasContinueBtn);
        }

        private void SetDeckType(DeckViewModel deckViewModel)
        {
            var currentDeckType = _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.CurrentDeck;

            _deckType = deckViewModel.SelectedDeck == DeckType.None && currentDeckType != DeckType.None
                ? currentDeckType
                : deckViewModel.SelectedDeck;
        }

        private void InitializeDeck(DeckViewModel deckViewModel)
        {
            if (IsDeckSelectedInProgress(deckViewModel))
            {
                LoadDeckFromProgress();
            }
            else
            {
                LoadDeckFromStaticData();
            }
        }

        private bool IsDeckSelectedInProgress(DeckViewModel deckViewModel)
        {
            return deckViewModel.SelectedDeck == DeckType.None &&
                   _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.CurrentDeck != DeckType.None;
        }

        private void LoadDeckFromProgress()
        {
            var cards = _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck;
            SpawnCards(cards);
        }

        private void LoadDeckFromStaticData()
        {
            DeckData deckData = _staticDataService.ForDeck(_deckType);
            _deckImage.sprite = deckData.CardBackImage;
            SpawnCards(_factory.CreateCards(deckData.Cards));
        }

        private void SpawnCards(List<Card> cards)
        {
            foreach (var cardData in cards)
            {
                CardView cardComponent = Instantiate(_cardPrefab, _cardGroup);
                cardComponent.Initialize(cardData,TeamType.None, false);
                _cardPopupService.SubscribeToCard(cardComponent);
            }
        }

        private void RegisterButtons() => 
            _continueBtn.onClick.AddListener(SetCurrentDeck);

        private void SetupContinueButton(bool hasContinueBtn) => 
            _continueBtn.gameObject.SetActive(hasContinueBtn);

        private void SetCurrentDeck()
        {
            _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.CurrentDeck = _deckType;
            _playerDeckService.CreateDeck(_staticDataService.ForDeck(_deckType).Cards);
            _permaDeckModel.SetHasContinueBtn(true);
            _saveLoadService.SaveProgress();
        }
    }
}
