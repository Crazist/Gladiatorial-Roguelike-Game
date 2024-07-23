using Data.Cards;
using Logic.Cards;
using Newtonsoft.Json;
using UI.View;

namespace Logic.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(CardConverter))]
    public abstract class Card
    {
        [JsonProperty] public CardData CardData { get; set; }

        public abstract void InitCard(CardData cardData);
        public abstract void InitializeView(DynamicCardView dynamicCardView);
    }
}