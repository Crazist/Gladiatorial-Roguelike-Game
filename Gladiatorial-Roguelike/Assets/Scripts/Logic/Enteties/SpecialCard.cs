using Data.Cards;
using Logic.Cards;
using Logic.Enteties;
using Logic.Types;
using Newtonsoft.Json;
using UI.View;
using UnityEngine;

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
            CardRarity = CardRarity.Normal;
            SpecialEffect = cardData.SpecialData.SpecialEffect;
            EffectValue = cardData.SpecialData.EffectValue;

            XpThreshold = cardData.SpecialData.XpThreshold;
            XpThresholdMultiplier = cardData.SpecialData.XPThresholdMultiplier;
            
            Level = 0;
            Xp = 0;
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);

        public override void LevelUp()
        {
            if (Level >= LevelMultiplierConfig.GetMaxLevel())
            {
                return;
            }

            Xp -= XpThreshold;
            if (Xp < 0)
            {
                Xp = 0;
            }

            Level++;
            XpThreshold = Mathf.RoundToInt(XpThreshold * XpThresholdMultiplier);

            if (Level % 4 == 0)
            {
                IncreaseRarity();
            }

            UpdateStats();
        }

        private void IncreaseRarity()
        {
            if (CardRarity < CardRarity.Legendary)
            {
                CardRarity++;
            }
        }
        private void UpdateStats()
        {
            float multiplier = LevelMultiplierConfig.GetMultiplierForLevel(Level);

            EffectValue = Mathf.RoundToInt(CardData.SpecialData.EffectValue * multiplier);
        }
    }
}