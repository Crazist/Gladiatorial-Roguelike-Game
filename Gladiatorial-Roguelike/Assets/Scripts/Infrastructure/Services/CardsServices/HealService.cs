using Logic.Enteties;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class HealService
    {
        private StaticDataService _staticData;

        [Inject]
        private void Inject(StaticDataService staticData) => 
            _staticData = staticData;

        public int CalculateHealCost(UnitCard unitCard) =>
            (unitCard.CardData.UnitData.Hp - unitCard.Hp) * _staticData.ForPriceSettings().HealPricePerHp;

        public void HealCard(UnitCard unitCard)
        {
            unitCard.Hp = unitCard.CardData.UnitData.Hp;
            unitCard.IsDead = false;
        }
    }
}