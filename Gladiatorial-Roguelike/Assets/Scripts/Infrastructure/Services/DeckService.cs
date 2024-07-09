using System.Collections.Generic;
using Logic.Cards;
using Logic.Enteties;
using Zenject;

namespace Infrastructure.Services
{
    public class DeckService
    {
        private List<Card> _currentDeck;
        
        private Factory _deckFactory;

        [Inject]
        public void Inject(Factory deckFactory) => 
            _deckFactory = deckFactory;

        public void InitializeDeck() => 
            _currentDeck = new List<Card>();

        public void AddCard(CardData cardData) => 
            _currentDeck.Add(_deckFactory.CreateCard(cardData));

        public List<Card> GetDeck() => 
            _currentDeck;

        public void RemoveCard(Card card) => 
            _currentDeck.Remove(card);

        public void ClearDeck() => 
            _currentDeck.Clear();
    }
}