using Data.Cards;
using DG.Tweening;
using Infrastructure.Services;
using Logic.Enteties;
using UI.View;
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
        private Card _card;
        private Tween _flipTween;
        private bool _isFaceUp = true;

        [Inject]
        private void Inject(StaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void Initialize(Card card)
        {
            _card = card;
            _cardData = card.CardData;
            UpdateCardDisplay();
        }

        public void FlipCard()
        {
            if (_flipTween != null && _flipTween.IsActive()) return;

            _flipTween = transform.DORotate(new Vector3(0, 90, 0), 0.3f)
                .OnComplete(() =>
                {
                    _isFaceUp = !_isFaceUp;
                    UpdateCardDisplay();
                    _dynamicCardView.HideStats(!_isFaceUp);
                    transform.DORotate(new Vector3(0, 0, 0), 0.3f);
                });
        }

        public void SetFaceDown()
        {
            if (_isFaceUp)
            {
                _isFaceUp = false;
                UpdateCardDisplay();
                _dynamicCardView.HideStats(true);
            }
        }

        public void UpgradeImage()
        {
            var sprite = _card.GetCurrentIcon();
            _cardImage.sprite = sprite != null ? sprite : _cardImage.sprite;
        }

        public bool IsFaceUp() => _isFaceUp;

        private void UpdateCardDisplay() =>
            _cardImage.sprite =
                _isFaceUp ? _card.GetCurrentIcon() : _staticDataService.ForDeck(_cardData.DeckType).CardBackImage;

        private void OnDestroy() => _flipTween?.Kill();
    }
}