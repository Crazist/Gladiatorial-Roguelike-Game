using Logic.Types;
using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Gladiatorial-Roguelike/Card/Create New Card")]
    public class CardData : ScriptableObject
    {
        public Sprite Icon;

        public string CardName;

        public int Attack;
        public int Defense;
        public int Hp;
        
        public CardType CardType;
        public CardRarity CardRarity;
    }
}