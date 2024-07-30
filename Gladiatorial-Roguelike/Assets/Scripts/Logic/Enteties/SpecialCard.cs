using Data.Cards;
using Logic.Cards;
using Logic.Enteties;
using Newtonsoft.Json;
using UI.View;

namespace Logic.Entities
{
    public class SpecialCard : Card
    {
        [JsonProperty] public string SpecialEffect { get; set; }
        [JsonProperty] public int EffectValue { get; set; }
       
        public override void InitCard(CardData cardData, LevelMultiplierConfig levelMultiplierConfig)
        {
            LevelMultiplierConfig = levelMultiplierConfig;
            CardData = cardData;
            SpecialEffect = cardData.SpecialData.SpecialEffect;
            EffectValue = cardData.SpecialData.EffectValue;
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);
    }
}