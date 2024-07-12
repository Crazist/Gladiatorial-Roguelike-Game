using System.Collections.Generic;
using Zenject;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;

namespace Infrastructure.Services
{
    public class PermaDeckService
    {
        private List<Card> _cards = new List<Card>();

        [Inject]
        private void Inject(PersistentProgressService persistentProgress) =>
            _cards = persistentProgress.PlayerProgress.Profile.PermaDeck.Cards;

        public void AddCardToDeck(Card card) => _cards.Add(card);

        public void RemoveCardFromDeck(Card card) => _cards.Remove(card);
       
        public List<Card> GetAllCards() => _cards;
        public void ClenUp() => _cards.Clear();
    }
}