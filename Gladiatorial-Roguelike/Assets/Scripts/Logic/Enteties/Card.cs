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
        [JsonProperty] public CardData CardData;
        [JsonProperty] public LevelMultiplierConfig LevelMultiplierConfig;
       
        [JsonProperty] public CardRarity CardRarity;
       
        [JsonProperty] public int Level;
        [JsonProperty] public int Xp;
        [JsonProperty] public int XpThreshold;
        [JsonProperty] public float XpThresholdMultiplier;
        [JsonProperty] public bool IsDead = false;

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