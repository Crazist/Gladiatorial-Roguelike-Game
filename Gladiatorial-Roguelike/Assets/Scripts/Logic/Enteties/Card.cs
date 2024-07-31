using Data.Cards;
using Logic.Types;
using Newtonsoft.Json;
using UI.View;
using UnityEngine;

namespace Logic.Enteties
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(CardConverter))]
    public abstract class Card
    {
        [JsonProperty] public CardData CardData { get; set; }
        [JsonProperty] public LevelMultiplierConfig LevelMultiplierConfig { get; set; }
        [JsonProperty] public CardRarity CardRarity { get; set; }
        [JsonProperty] public int Level { get; set; }
        [JsonProperty] public int Xp { get; set; }
        [JsonProperty] public int XpThreshold { get; set; }
        [JsonProperty] public float XpThresholdMultiplier { get; set; }
        [JsonProperty] public bool IsDead { get; set; } = false;

        public abstract void InitCard(CardData cardData, LevelMultiplierConfig levelMultiplierConfig);
        public abstract void InitializeView(DynamicCardView dynamicCardView);
        public abstract void LevelUp();

        public void GainXP(int xp)
        {
            Xp += xp;

            while (Xp >= XpThreshold && Level < LevelMultiplierConfig.GetMaxLevel())
            {
                LevelUp();
            }
        }

        public Sprite GetCurrentIcon()
        {
            int index = (int)CardRarity;
            if (index >= 0 && index < CardData.RarityIcons.Count)
            {
                return CardData.RarityIcons[index];
            }

            return null;
        }
    }
}