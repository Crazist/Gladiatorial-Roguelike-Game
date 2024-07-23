using Data.Cards;
using Logic.Cards;
using Newtonsoft.Json;
using UI.View;

namespace Logic.Entities
{
    public class UnitCard : Card
    {
        [JsonProperty]
        public int Attack { get; set; }

        [JsonProperty]
        public int Defense { get; set; }

        [JsonProperty]
        public int Hp { get; set; }

        [JsonProperty]
        public int XP { get; set; }

        public override void InitCard(CardData cardData)
        {
            CardData = cardData;
            Attack = cardData.UnitData.Attack;
            Defense = cardData.UnitData.Defense;
            Hp = cardData.UnitData.Hp;
            XP = cardData.UnitData.XP;
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);
    }
}