using Infrastructure.Services;
using Logic.Cards;
using Logic.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] private Image _cardImage;
        [SerializeField] private DynamicCardView _dynamicCardView;

        private StaticDataService _staticDataService;
        private CardData _cardData;
        private bool _isFaceUp = true;

        [Inject]
        private void Inject(StaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void Initialize(Card card)
        {
            _cardData = card.CardData;
            UpdateCardDisplay();

            if (card is UnitCard unitCard)
            {
                _dynamicCardView.Initialize(unitCard);
            }
            else if (card is SpecialCard specialCard)
            {
                _dynamicCardView.Initialize(specialCard);
            }
        }

        public void FlipCard()
        {
            _isFaceUp = !_isFaceUp;
            UpdateCardDisplay();
            _dynamicCardView.HideStats(!_isFaceUp);
        }

        public bool IsFaceUp() => _isFaceUp;
        private void UpdateCardDisplay() =>
            _cardImage.sprite =
                _isFaceUp ? _cardData.Icon : _staticDataService.ForDeck(_cardData.DeckType).CardBackImage;
    }
}