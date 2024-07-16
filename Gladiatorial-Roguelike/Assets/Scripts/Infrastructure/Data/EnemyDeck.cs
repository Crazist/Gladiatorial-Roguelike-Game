using System;
using System.Collections.Generic;
using Logic.Entities;

namespace Infrastructure.Data
{
    [Serializable]
    public class EnemyDeck
    {
        public List<Card> Cards;
        public Reward Reward;
        public EnemyDeckState IsSkipped;
        public EnemyDeck()
        {
            Cards = new List<Card>();
            IsSkipped = EnemyDeckState.None;
        }
    }
}