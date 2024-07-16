using System.Collections.Generic;
using Zenject;
using Infrastructure.Services.PersistentProgress;
using Logic.Entities;

namespace Infrastructure.Services
{
    public class PermaDeckService
    {
        private PersistentProgressService _persistentProgress;

        [Inject]
        private void Inject(PersistentProgressService persistentProgress) => 
            _persistentProgress = persistentProgress;

        public void AddCardToDeck(Card card) => _persistentProgress.PlayerProgress.DeckProgress.PermaDeck.Cards.Add(card);

        public void RemoveCardFromDeck(Card card) => _persistentProgress.PlayerProgress.DeckProgress.PermaDeck.Cards.Remove(card);
       
        public List<Card> GetAllCards() => _persistentProgress.PlayerProgress.DeckProgress.PermaDeck.Cards;
        public void ClenUp() => _persistentProgress.PlayerProgress.DeckProgress.PermaDeck.Cards.Clear();
    }
}