using Infrastructure.Services.CardsServices;
using UI.View;
using Zenject;

namespace UI.Elements.CardDrops
{
    public class UpgradeCardDropArea : CardDropArea
    {
        private UpgradeService _upgradeService;

        [Inject]
        public void Construct(UpgradeService upgradeService) =>
            _upgradeService = upgradeService;

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            _upgradeService.UpgradeCard(cardView.GetCard());
            cardView.GetDynamicCardView().UpdateCard();
            cardView.GetCardDisplay().UpgradeImage();

            cardDragService.ResetPosition(cardView);
        }
    }
}