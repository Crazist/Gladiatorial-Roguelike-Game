using Logic.Enteties;
using Logic.Entities;
using UI.View;

namespace Infrastructure.Services.CardsServices
{
    public class HealService
    {
        public void HealCard(CardView cardView)
        {
            var card = cardView.GetCard();

            if (card is UnitCard unitCard)
            {
                unitCard.Hp = card.CardData.UnitData.Hp;
                card.IsDead = false;
            }
            
            cardView.GetDynamicCardView().UpdateHp();
        }
    }
}