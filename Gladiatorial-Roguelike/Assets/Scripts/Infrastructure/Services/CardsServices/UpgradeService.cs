using Logic.Entities;
using UnityEngine;

namespace Infrastructure.Services.CardsServices
{
    public class UpgradeService
    {
        public void UpgradeCard(UnitCard unitCard)
        {
            unitCard.LevelUp();
        }
    }
}