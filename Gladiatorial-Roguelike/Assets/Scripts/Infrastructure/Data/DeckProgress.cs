using System;
using Infrastructure.Services;

namespace Infrastructure.Data
{
    [Serializable]
    public class DeckProgress
    {
        public PermaDeck PermaDeck;
        public DeckType CurrentDeck;

        public DeckProgress() => 
            PermaDeck = new PermaDeck();
    }
}