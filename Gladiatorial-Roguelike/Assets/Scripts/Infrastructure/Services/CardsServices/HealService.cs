using Logic.Enteties;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class HealService
    {
        private StaticDataService _staticData;

        [Inject]
        private void Inject(StaticDataService staticData) => 
            _staticData = staticData;

        public int CalculateHealCost(UnitCard unitCard)
        {
            float multiplier = unitCard.LevelMultiplierConfig.GetMultiplierForLevel(unitCard.Level);
            int maxHp = Mathf.RoundToInt(unitCard.CardData.UnitData.Hp * multiplier);
            int missingHp = maxHp - unitCard.Hp;
            return missingHp * _staticData.ForPriceSettings().HealPricePerHp;
        }

        public void HealCard(UnitCard unitCard)
        {
            float multiplier = unitCard.LevelMultiplierConfig.GetMultiplierForLevel(unitCard.Level);
            int maxHp = Mathf.RoundToInt(unitCard.CardData.UnitData.Hp * multiplier);
            unitCard.Hp = maxHp;
            unitCard.IsDead = false;
        }
    }
}