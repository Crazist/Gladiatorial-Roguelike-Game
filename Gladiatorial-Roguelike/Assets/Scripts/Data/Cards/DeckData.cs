using Data.Cards;
using Infrastructure.Services;
using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "RomanDeck", menuName = "Gladiatorial-Roguelike/Deck/Create New Deck")]
    public class DeckData : ScriptableObject
    {
        public DeckType DeckType;
        
        public Sprite CardBackImage;
        public Sprite CardFrontImage;
        
        public CardData[] Cards;
    }
}