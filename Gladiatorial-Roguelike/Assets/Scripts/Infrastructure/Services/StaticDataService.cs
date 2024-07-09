using Logic.Cards;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService
    {
        public DeckData Deck;
        
        private const string deckResourcePath = "Data/Decks/RomanDeck";

        public StaticDataService()
        {
            LoadRomanDeck();
        }

        public DeckData ToRomanDeck() => Deck;

        private void LoadRomanDeck() => Deck = Resources.Load<DeckData>(deckResourcePath);
    }
}