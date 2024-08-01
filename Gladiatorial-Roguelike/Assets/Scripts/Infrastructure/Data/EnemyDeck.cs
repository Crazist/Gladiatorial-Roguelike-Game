using System;
using System.Collections.Generic;
using Logic.Enteties;
using Logic.Types;

namespace Infrastructure.Data
{
    [Serializable]
    public class EnemyDeck
    {
        public List<Card> Cards;
        public EnemyDeckState IsSkipped;
        public DeckComplexity DeckComplexity;
        public EnemyDeck(DeckComplexity deckComplexity)
        {
            Cards = new List<Card>();
            IsSkipped = EnemyDeckState.None;
            DeckComplexity = deckComplexity;
        }
    }
}