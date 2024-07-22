using Infrastructure.Services;
using UnityEngine;

namespace Data.Cards
{
    [CreateAssetMenu(fileName = "FractionDeck", menuName = "Gladiatorial-Roguelike/Deck/Create New FractionDeck")]
    public class FractionDeckData : ScriptableObject
    {
        public DeckType DeckType;
        public CardData[] Cards;
    }
}