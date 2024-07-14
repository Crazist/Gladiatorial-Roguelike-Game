using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Enteties;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class CardSellService
    {
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService) => 
            _persistentProgressService = persistentProgressService;

        public void SellCard(CardView cardView)
        {
            CardData cardData = cardView.GetCardData();
            
            string cardName = cardData.CardName;

            List<Card> cards = _persistentProgressService.PlayerProgress.Profile.PermaDeck.Cards;
           
            Card cardToRemove = cards.FirstOrDefault(card => card.CardName == cardName);

            if (cardToRemove != null)
            {
                cards.Remove(cardToRemove);
                Object.Destroy(cardView.gameObject);
            }
        }
    }
}