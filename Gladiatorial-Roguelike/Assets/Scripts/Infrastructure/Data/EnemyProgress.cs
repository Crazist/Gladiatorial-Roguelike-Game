using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Entities;

namespace Infrastructure.Data
{
    [Serializable]
    public class EnemyProgress
    {
        public List<Card> EasyDeck;
        public List<Card> IntermediateDeck;
        public List<Card> HardDeck;

        public DeckType EnemyDeckType;
    }
}