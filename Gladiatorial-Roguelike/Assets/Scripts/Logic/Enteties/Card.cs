using Data.Cards;
using Logic.Types;
using Newtonsoft.Json;
using UI.View;

namespace Logic.Enteties
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(CardConverter))]
    public abstract class Card
    {
        [JsonProperty] public CardData CardData { get; set; }
        [JsonProperty] public LevelMultiplierConfig LevelMultiplierConfig { get; set; }
        [JsonProperty] public CardRarity CardRarity { get; set; }
        [JsonProperty] public int Level { get;  set; }
        [JsonProperty] public bool IsDead { get; set; } = false;

        public abstract void InitCard(CardData cardData, LevelMultiplierConfig levelMultiplierConfig);
        public abstract void InitializeView(DynamicCardView dynamicCardView);
    }
}