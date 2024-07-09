using System.Collections.Generic;
using Logic.Enteties;
using Zenject;

namespace Infrastructure.Services
{
    public class DeckService
    {
        private List<Card> _currentDeck;
        
        private Factory _deckFactory;

        [Inject]
        public void Inject(Factory deckFactory)
        {
            _deckFactory = deckFactory;
          
            _currentDeck = new List<Card>();
        }

        public void InitializeDeck()
        {
            _currentDeck = _deckFactory.CreateDeck();
        }

        public List<Card> GetDeck()
        {
            return _currentDeck;
        }

        public void AddCard(Card card)
        {
            _currentDeck.Add(card);
        }

        public void RemoveCard(Card card)
        {
            _currentDeck.Remove(card);
        }

        public void ClearDeck()
        {
            _currentDeck.Clear();
        }
    }
}