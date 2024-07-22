using System.Collections.Generic;
using Data.Cards;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Entities;
using Zenject;

namespace Infrastructure.Services
{
    public class PlayerDeckService
    {
        private List<Card> _currentDeck;
        
        private Factory _deckFactory;
        private PersistentProgressService _persistentProgress;

        [Inject]
        public void Inject(Factory deckFactory, PersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
            _deckFactory = deckFactory;
        }

        public void CreateDeck(CardData[] deck)
        {
            _currentDeck = _deckFactory.CreateCards(deck);
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck = _currentDeck;
        }

        public void LoadDeck(List<Card> deck) => 
            _currentDeck = deck;

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