using Infrastructure.Services.CardsServices;
using UI.Services;
using UI.View;
using UnityEngine;

namespace UI.Elements.CardDrops
{
    public class UpgradeCardDropArea : CardDropArea
    {
        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            Debug.Log("Card healed");
        }
    }
}