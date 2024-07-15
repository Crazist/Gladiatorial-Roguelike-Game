using Infrastructure.Services;
using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Gladiatorial-Roguelike/Card/Create New Card")]
    public class FractionDeckData : ScriptableObject
    {
        public DeckType DeckType;
        public CardData[] Cards;
    }
}