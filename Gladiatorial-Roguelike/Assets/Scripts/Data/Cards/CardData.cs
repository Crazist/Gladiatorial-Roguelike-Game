using System;
using Infrastructure.Services;
using Logic.Types;
using UnityEngine;

namespace Logic.Cards
{
    [Serializable]
    public abstract class CardData : ScriptableObject
    {
        public Sprite Icon;
       
        public string CardName;
       
        public CardType CardType;
        public CardRarity CardRarity;
        public CardCategory Category;
        public DeckType DeckType;
    }
}