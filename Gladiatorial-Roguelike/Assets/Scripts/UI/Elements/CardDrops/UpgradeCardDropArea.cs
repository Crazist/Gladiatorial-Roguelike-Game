using Infrastructure.Services.CardsServices;
using Infrastructure.Services.Currency;
using Logic.Enteties;
using UI.View;
using Zenject;
using UI.Popup;

namespace UI.Elements.CardDrops
{
    public class UpgradeCardDropArea : CardDropArea
    {
        private UpgradeService _upgradeService;
        private CurrencyService _currencyService;
        private ConfirmationPopup _confirmationPopup;

        [Inject]
        private void Inject(UpgradeService upgradeService, CurrencyService currencyService, ConfirmationPopup confirmationPopup)
        {
            _upgradeService = upgradeService;
            _currencyService = currencyService;
            _confirmationPopup = confirmationPopup;
        }

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            var card = cardView.GetCard();
            int upgradeCost = _upgradeService.CalculateUpgradeCost(card);

            _confirmationPopup.Show(
                $"Are you sure you want to upgrade this card for {upgradeCost} gold?",
                () => ConfirmUpgradeCard(card, cardDragService, cardView, upgradeCost),
                () => cardDragService.ResetPosition(cardView)
            );
        }

        private void ConfirmUpgradeCard(Card card, CardDragService cardDragService, CardView cardView, int upgradeCost)
        {
            if (_currencyService.GetCurrency() >= upgradeCost)
            {
                _currencyService.SpendCurrency(upgradeCost);
                _upgradeService.UpgradeCard(card);
                cardView.GetDynamicCardView().UpdateCard();
                cardView.GetCardDisplay().UpgradeImage();
            }

            cardDragService.ResetPosition(cardView);
        }
    }
}