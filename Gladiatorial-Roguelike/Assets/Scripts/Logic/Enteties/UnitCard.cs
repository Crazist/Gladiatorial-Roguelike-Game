using System;
using Logic.Cards;

namespace Logic.Entities
{
    [Serializable]
    public class UnitCard : Card
    {
        public int Attack;
        public int Defense;
        public int Hp;
        public int XP;

        public UnitCard(UnitCardData data) : base(data)
        {
            Attack = data.Attack;
            Defense = data.Defense;
            Hp = data.Hp;
            XP = data.XP;
        }
    }
}