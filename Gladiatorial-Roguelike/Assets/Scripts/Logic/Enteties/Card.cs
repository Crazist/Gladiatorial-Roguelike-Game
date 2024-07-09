using Logic.Cards;
using Logic.Types;
using UnityEngine;

namespace Logic.Enteties
{
    public class Card
    {
        public Sprite Icon;
       
        public string CardName;
      
        public int Attack;
        public int Defense;
        public int Hp;
        
        public CardType CardType;
        public CardRarity CardRarity;

        public Card(CardData data)
        {
            Icon = data.Icon;
            CardName = data.CardName;
            Attack = data.Attack;
            Defense = data.Defense;
            Hp = data.Hp;
            CardType = data.CardType;
            CardRarity = data.CardRarity;
        }
    }
}