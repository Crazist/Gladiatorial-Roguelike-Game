using System;
using Logic.Cards;
using Logic.Types;

namespace Logic.Enteties
{
    [Serializable]
    public class Card
    {
        public string CardName;
      
        public int Attack;
        public int Defense;
        public int Hp;
        
        public CardType CardType;
        public CardRarity CardRarity;

        public CardData CardData;
        public Card(CardData data)
        {
            CardData = data;
            
            CardName = data.CardName;
            Attack = data.Attack;
            Defense = data.Defense;
            Hp = data.Hp;
            CardType = data.CardType;
            CardRarity = data.CardRarity;
        }
    }
}