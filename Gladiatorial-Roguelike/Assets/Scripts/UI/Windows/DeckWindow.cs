using Infrastructure.Services;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using UI.Elements;
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

        [Inject]
        private void Inject(DeckViewModel deckViewModel, StaticDataService staticDataService,
            CardPopupService cardPopupService, PersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;
            _staticDataService = staticDataService;
            _deckType = deckViewModel.SelectedDeck;

            InitializeDeck();
            BtnRegister();
        }

        private void InitializeDeck()
        {
            DeckData deckData = _staticDataService.ForDeck(_deckType);
            _deckImage.sprite = deckData.CardBackImage;
            SpawnCards(deckData.Cards);
        }

        private void BtnRegister() => 
            _continueBtn.onClick.AddListener(SetCurrentDeck);

        private void SpawnCards(CardData[] cards)
        {
            foreach (var cardData in cards)
            {
                CardView cardComponent = Instantiate(_cardPrefab, _cardGroup);
                cardComponent.Initialize(cardData, false);

                _cardPopupService.SubscribeToCard(cardComponent);
            }
        }

        private void SetCurrentDeck() => 
            _persistentProgressService.PlayerProgress.DeckProgress.CurrentDeck = _deckType;
    }
}