using Infrastructure.Services;
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

        private DeckType _deckType;
        private StaticDataService _staticDataService;
        private CardPopup _cardPopup;

        [Inject]
        private void Inject(DeckViewModel deckViewModel, StaticDataService staticDataService, CardPopup cardPopup)
        {
            _cardPopup = cardPopup;
            _staticDataService = staticDataService;
            _deckType = deckViewModel.SelectedDeck;

            InitializeDeck();
        }

        private void InitializeDeck()
        {
            DeckData deckData = _staticDataService.ForDeck(_deckType);
            _deckImage.sprite = deckData.CardBackImage;
            SpawnCards(deckData.Cards);
        }

        private void SpawnCards(CardData[] cards)
        {
            foreach (var cardData in cards)
            {
                CardView cardComponent = Instantiate(_cardPrefab, _cardGroup);
                cardComponent.Initialize(cardData);

                cardComponent.OnCardHoverEnter += HandleCardHoverEnter;
                cardComponent.OnCardHoverExit += HandleCardHoverExit;
            }
        }

        private void HandleCardHoverEnter(CardView cardView) =>
            _cardPopup.Show(cardView.transform.position + new Vector3(100, 0, 0), cardView.GetCardData());

        private void HandleCardHoverExit(CardView cardView) =>
            _cardPopup.Hide();
    }
}