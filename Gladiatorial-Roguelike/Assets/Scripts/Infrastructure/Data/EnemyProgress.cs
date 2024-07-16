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
            EasyDeck = new EnemyDeck();
            IntermediateDeck = new EnemyDeck();
            HardDeck = new EnemyDeck();
        }
    }
}