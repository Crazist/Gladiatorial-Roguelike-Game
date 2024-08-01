using System;
using Infrastructure.Services;
using Logic.Types;

namespace Infrastructure.Data
{
    [Serializable]
    public class EnemyProgress
    {
        public EnemyDeck EasyDeck;
        public EnemyDeck IntermediateDeck;
        public EnemyDeck HardDeck;

        public DeckType EnemyDeckType;
        public DeckComplexity ChoosenDeck;
        public EnemyProgress() => 
            RefreshEnemy();

        public void RefreshEnemy()
        {
            EasyDeck = new EnemyDeck(DeckComplexity.Easy);
            IntermediateDeck = new EnemyDeck(DeckComplexity.Intermediate);
            HardDeck = new EnemyDeck(DeckComplexity.Hard);
        }
    }
}