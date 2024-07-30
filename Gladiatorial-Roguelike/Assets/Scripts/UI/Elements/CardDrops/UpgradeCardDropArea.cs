using Infrastructure.Services.CardsServices;
using Logic.Entities;
using UI.View;
using Zenject;

namespace UI.Elements.CardDrops
{
    public class UpgradeCardDropArea : CardDropArea
    {
        private UpgradeService _upgradeService;

        [Inject]
        public void Construct(UpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            if (cardView.GetCard() is UnitCard unitCard)
            {
                _upgradeService.UpgradeCard(unitCard);
                cardView.GetDynamicCardView().UpdateHp();
            }
            Destroy(cardView.gameObject);
        }
    }
}