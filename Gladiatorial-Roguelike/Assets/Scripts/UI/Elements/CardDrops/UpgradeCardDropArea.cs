using Infrastructure.Services.CardsServices;
using UI.View;

namespace UI.Elements.CardDrops
{
    public class UpgradeCardDropArea : CardDropArea
    {
        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            Destroy(cardView.gameObject);
        }
    }
}