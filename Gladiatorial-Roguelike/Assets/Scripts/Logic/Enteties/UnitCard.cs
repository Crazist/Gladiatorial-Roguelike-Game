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
        [JsonProperty] public int Xp { get; set; }
        [JsonProperty] private int XpThreshold { get; set; }
        [JsonProperty] private float XpThresholdMultiplier { get; set; }

        public override void InitCard(CardData cardData, LevelMultiplierConfig levelMultiplierConfig)
        {
            LevelMultiplierConfig = levelMultiplierConfig;

            CardData = cardData;
            Level = 0;
            CardRarity = CardRarity.Normal;
            Xp = cardData.UnitData.XP;
            XpThreshold = cardData.UnitData.XpThreshold;
            XpThresholdMultiplier = cardData.UnitData.XPThresholdMultiplier;

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

        public void GainXP(int xp)
        {
            Xp += xp;

            if (Xp >= XpThreshold)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Xp -= XpThreshold;
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
            Xp = 0;
        }
    }
}