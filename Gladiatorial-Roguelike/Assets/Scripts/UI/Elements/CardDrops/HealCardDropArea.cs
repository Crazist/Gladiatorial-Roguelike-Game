using Infrastructure.Services.CardsServices;
using Infrastructure.Services.Currency;
using Logic.Enteties;
using UI.View;
using Zenject;
using UI.Popup;

namespace UI.Elements.CardDrops
{
    public class HealCardDropArea : CardDropArea
    {
        private HealService _healService;
        private CurrencyService _currencyService;
        private ConfirmationPopup _confirmationPopup;

        [Inject]
        private void Inject(HealService healService, CurrencyService currencyService, ConfirmationPopup confirmationPopup)
        {
            _healService = healService;
            _currencyService = currencyService;
            _confirmationPopup = confirmationPopup;
        }

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            var card = cardView.GetCard();

            if (card is UnitCard unitCard)
            {
                int healCost = _healService.CalculateHealCost(unitCard);

                _confirmationPopup.Show(
                    $"Are you sure you want to heal this card for {healCost} gold?",
                    () => ConfirmHealCard(unitCard, cardDragService, cardView, healCost),
                    () => cardDragService.ResetPosition(cardView)
                );
            }
            else
            {
                cardDragService.ResetPosition(cardView);
            }
        }

        private void ConfirmHealCard(UnitCard unitCard, CardDragService cardDragService, CardView cardView, int healCost)
        {
            if (_currencyService.GetCurrency() >= healCost)
            {
                _currencyService.SpendCurrency(healCost);
                _healService.HealCard(unitCard);
                cardView.GetDynamicCardView().UpdateCard();
            }

            cardDragService.ResetPosition(cardView);
        }
    }
}