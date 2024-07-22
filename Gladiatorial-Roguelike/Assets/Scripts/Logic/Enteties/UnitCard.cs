using Logic.Cards;
using Newtonsoft.Json;

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
            if (cardData is UnitCardData unitCardData)
            {
                CardData = unitCardData;
                Attack = unitCardData.Attack;
                Defense = unitCardData.Defense;
                Hp = unitCardData.Hp;
                XP = unitCardData.XP;
            }
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);
    }
}