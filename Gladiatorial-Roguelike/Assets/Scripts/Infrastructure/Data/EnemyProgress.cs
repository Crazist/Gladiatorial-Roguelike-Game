using System;
using Infrastructure.Services;

namespace Infrastructure.Data
{
    [Serializable]
    public class EnemyProgress
    {
        public EnemyDeck EasyDeck;
        public EnemyDeck IntermediateDeck;
        public EnemyDeck HardDeck;

        public DeckType EnemyDeckType;

        public EnemyProgress()
        {
            EasyDeck = new EnemyDeck(DeckComplexity.Easy);
            IntermediateDeck = new EnemyDeck(DeckComplexity.Intermediate);
            HardDeck = new EnemyDeck(DeckComplexity.Hard);
        }
    }
}