using System;
using Logic.Cards;
using Logic.Types;

namespace Logic.Entities
{
    [Serializable]
    public abstract class Card
    {
        public string CardName;
        public CardType CardType;
        public CardRarity CardRarity;
        public CardCategory Category;

        public CardData CardData;

        public Card(CardData data)
        {
            CardData = data;
            CardName = data.CardName;
            CardType = data.CardType;
            CardRarity = data.CardRarity;
            Category = data.Category;
        }
    }
}