using Logic.Enteties;

namespace Infrastructure.Services.CardsServices
{
    public class UpgradeService
    {
        public void UpgradeCard(Card card) => 
            card.LevelUp();
    }
}