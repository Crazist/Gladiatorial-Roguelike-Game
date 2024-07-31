using Data.Cards;
using Logic.Types;
using Newtonsoft.Json;
using UI.View;
using UnityEngine;

namespace Logic.Enteties
{
    public class UnitCard : Card
    {
        [JsonProperty] public int Attack { get; set; }
        [JsonProperty] public int Defense { get; set; }
        [JsonProperty] public int Hp { get; set; }

        public override void InitCard(CardData cardData, LevelMultiplierConfig levelMultiplierConfig)
        {
            LevelMultiplierConfig = levelMultiplierConfig;
            CardData = cardData;
            CardRarity = CardRarity.Normal;
            XpThreshold = cardData.UnitData.XpThreshold;
            XpThresholdMultiplier = cardData.UnitData.XPThresholdMultiplier;
            Xp = cardData.UnitData.XP;

            Level = 0;

            UpdateStats();
        }

        public override void InitializeView(DynamicCardView dynamicCardView) =>
            dynamicCardView.Initialize(this);

        public void TakeDamage(int damage)
        {
            Hp -= damage;

            if (Hp <= 0)
            {
                Hp = 0;
                IsDead = true;
            }
        }

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

            Attack = Mathf.RoundToInt(CardData.UnitData.Attack * multiplier);
            Defense = Mathf.RoundToInt(CardData.UnitData.Defense * multiplier);
            Hp = Mathf.RoundToInt(CardData.UnitData.Hp * multiplier);
        }
    }
}