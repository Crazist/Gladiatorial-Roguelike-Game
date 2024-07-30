using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Enteties;
using Logic.Entities;

namespace Infrastructure.Data
{
    [Serializable]
    public class DeckProgress
    {
        public List<Card> PlayerDeck;
        public DeckType CurrentDeck;
    }
}