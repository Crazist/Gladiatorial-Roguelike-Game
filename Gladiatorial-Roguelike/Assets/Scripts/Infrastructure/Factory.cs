using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Cards;
using Logic.Enteties;

using Zenject;

namespace Infrastructure
{
    public class Factory
    {
        private StaticDataService _staticDataService;

        [Inject]
        private void Inject(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public List<Card> CreateDeck()
        {
            DeckData deckData = _staticDataService.ToRomanDeck();

            List<Card> deck = new List<Card>();

            foreach (CardData cardData in deckData.Cards)
            {
                deck.Add(new Card(cardData));
            }

            return deck;
        }
    }
}