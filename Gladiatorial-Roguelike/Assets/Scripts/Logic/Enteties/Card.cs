using Data.Cards;
using Newtonsoft.Json;
using UI.View;

namespace Logic.Enteties
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(CardConverter))]
    public abstract class Card
    {
        [JsonProperty] public CardData CardData { get; set; }
        [JsonProperty] public bool IsDead { get; set; } = false;

        public abstract void InitCard(CardData cardData);
        public abstract void InitializeView(DynamicCardView dynamicCardView);
    }
}