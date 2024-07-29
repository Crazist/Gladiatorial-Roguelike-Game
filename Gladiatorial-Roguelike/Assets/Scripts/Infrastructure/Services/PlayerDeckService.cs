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
        private Factory _deckFactory;
        private PersistentProgressService _persistentProgress;

        [Inject]
        public void Inject(Factory deckFactory, PersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
            _deckFactory = deckFactory;
        }

        public void CreateDeck(CardData[] deck) => 
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck =  _deckFactory.CreateCards(deck);

        public void AddCard(CardData cardData) => 
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck.Add(_deckFactory.CreateCard(cardData));

        public List<Card> GetDeck() => 
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck;

        public void RemoveCard(Card card) => 
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck.Remove(card);

        public void ClearDeck() => 
            _persistentProgress.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck.Clear();
    }
}