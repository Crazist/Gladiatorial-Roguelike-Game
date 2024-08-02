using System.Collections.Generic;
using Zenject;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;

namespace Infrastructure.Services
{
    public class PermaDeckService
    {
        private PersistentProgressService _persistentProgress;

        [Inject]
        private void Inject(PersistentProgressService persistentProgress) => 
            _persistentProgress = persistentProgress;

        public void AddCardToDeck(Card card) => 
            _persistentProgress.PlayerProgress.PermaDeck.Cards.Add(card);

        public void RemoveCardFromDeck(Card card) => 
            _persistentProgress.PlayerProgress.PermaDeck.Cards.Remove(card);
       
        public List<Card> GetAllCards() => 
            _persistentProgress.PlayerProgress.PermaDeck.Cards;
        public void ClenUp() => 
            _persistentProgress.PlayerProgress.PermaDeck.Cards.Clear();
    }
}