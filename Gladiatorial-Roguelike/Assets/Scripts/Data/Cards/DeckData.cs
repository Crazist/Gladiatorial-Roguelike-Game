using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "RomanDeck", menuName = "Gladiatorial-Roguelike/Deck/Create New Deck")]
    public class DeckData : ScriptableObject
    {
        public CardData[] Cards;
    }
}