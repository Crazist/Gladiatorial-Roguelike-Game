using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Entities;
using UI.Elements;
using Zenject;

namespace UI.Services
{
    public class CardSellService
    {
        private PersistentProgressService _persistentProgressService;
        private ConfirmationPopup _confirmationPopup;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, ConfirmationPopup confirmationPopup)
        {
            _persistentProgressService = persistentProgressService;
            _confirmationPopup = confirmationPopup;
        }

        public void SellCard(CardView cardView, Action refreshPos) => 
            _confirmationPopup.Show(() => ConfirmSellCard(cardView), refreshPos);

        private void ConfirmSellCard(CardView cardView)
        {
            CardData cardData = cardView.GetCardData();
            string cardName = cardData.CardName;

            List<Card> cards = _persistentProgressService.PlayerProgress.DeckProgress.PermaDeck.Cards;
            
            Card cardToRemove = cards.FirstOrDefault(card => card.CardData.CardName == cardName);

            if (cardToRemove != null)
            {
                cards.Remove(cardToRemove);
                UnityEngine.Object.Destroy(cardView.gameObject);
            }
        }
    }
}