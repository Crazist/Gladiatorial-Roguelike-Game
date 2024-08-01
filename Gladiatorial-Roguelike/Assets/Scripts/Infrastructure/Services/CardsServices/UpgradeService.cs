using Logic.Enteties;

namespace Infrastructure.Services.CardsServices
{
    public class UpgradeService
    {
        private readonly StaticDataService _staticData;

        public UpgradeService(StaticDataService staticData) => 
            _staticData = staticData;

        public int CalculateUpgradeCost(Card card) => 
            (card.Level + 1) * _staticData.ForPriceSettings().UpgradePriceMultiplier;

        public void UpgradeCard(Card card) => 
            card.LevelUp();
    }
}