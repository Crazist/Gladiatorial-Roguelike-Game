using Infrastructure.Services.CardsServices;
using Logic.Entities;
using UI.View;
using Zenject;

namespace UI.Elements.CardDrops
{
    public class HealCardDropArea : CardDropArea
    {
        private HealService _healService;

        [Inject]
        private void Inject(HealService healService) => 
            _healService = healService;

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        { 
            _healService.HealCard(cardView);
            cardDragService.ResetPosition(cardView);
        }
    }
}