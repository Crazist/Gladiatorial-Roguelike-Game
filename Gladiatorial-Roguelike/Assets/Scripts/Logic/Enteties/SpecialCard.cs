using Logic.Cards;
using Newtonsoft.Json;

namespace Logic.Entities
{
    public class SpecialCard : Card
    {
        [JsonProperty]
        public string SpecialEffect { get; set; }
        [JsonProperty]
        public int EffectValue { get; set; }

        public override void InitCard(CardData cardData)
        {
            if (cardData is SpecialCardData specialCardData)
            {
                CardData = specialCardData;
                SpecialEffect = specialCardData.SpecialEffect;
                EffectValue = specialCardData.EffectValue;
            }
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);
    }
}