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
        public bool IsSkipped;

        public EnemyDeck()
        {
            Cards = new List<Card>();
            IsSkipped = false;
        }
    }
}