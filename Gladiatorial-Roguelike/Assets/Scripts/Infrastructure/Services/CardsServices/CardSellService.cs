using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cards;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Enteties;
using Logic.Entities;
using UI.Elements;
using UI.View;
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
            CardData cardData = cardView.GetCard().CardData;
            string cardName = cardData.CardName;

            List<Card> cards = _persistentProgressService.PlayerProgress.PermaDeck.Cards;
            
            Card cardToRemove = cards.FirstOrDefault(card => card.CardData.CardName == cardName);

            if (cardToRemove != null)
            {
                cards.Remove(cardToRemove);
            }
            
            UnityEngine.Object.Destroy(cardView.gameObject);
        }
    }
}