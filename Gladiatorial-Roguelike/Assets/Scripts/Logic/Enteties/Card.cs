using Logic.Cards;

namespace Logic.Entities
{
    public abstract class Card
    {
        public CardData CardData { get; }

        protected Card(CardData cardData)
        {
            CardData = cardData;
        }

        public abstract void InitializeView(DynamicCardView dynamicCardView);
    }
}