using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "New Unit Card", menuName = "Gladiatorial-Roguelike/Card/Create New Unit Card")]
    public class UnitCardData : CardData
    {
        public int Attack;
        public int Defense;
        public int Hp;
        public int XP;
    }
}