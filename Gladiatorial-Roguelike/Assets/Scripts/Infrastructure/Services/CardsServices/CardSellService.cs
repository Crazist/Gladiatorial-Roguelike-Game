using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Cards;
using Infrastructure.Services.Currency;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using UI.Popup;
using UI.View;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class CardSellService
    {
        private PersistentProgressService _persistentProgressService;
        private ConfirmationPopup _confirmationPopup;
        private CurrencyService _currencyService;
        private StaticDataService _staticData;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, ConfirmationPopup confirmationPopup,
            CurrencyService currencyService, StaticDataService staticData)
        {
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
            _confirmationPopup = confirmationPopup;
            _currencyService = currencyService;
        }

        public void SellCard(CardView cardView, Action refreshPos)
        {
            int sellPrice = _staticData.ForPriceSettings().GetPriceForRarity(cardView.GetCard().CardRarity);
            _confirmationPopup.Show($"Are you sure you want to sell this card for {sellPrice} gold?",
                () => ConfirmSellCard(cardView, sellPrice), refreshPos);
        }

        private void ConfirmSellCard(CardView cardView, int sellPrice)
        {
            CardData cardData = cardView.GetCard().CardData;
            string cardName = cardData.CardName;

            List<Card> cards = _persistentProgressService.PlayerProgress.PermaDeck.Cards;
            List<Card> playerCards = _persistentProgressService.PlayerProgress.CurrentRun.DeckProgress.PlayerDeck;

            Card cardToRemove = cards.FirstOrDefault(card => card.CardData.CardName == cardName);
            Card removePlayerCard = null;

            if (playerCards != null)
            {
                removePlayerCard = playerCards.FirstOrDefault(card => card.CardData.CardName == cardName);
            }

            if (cardToRemove != null)
            {
                cards.Remove(cardToRemove);
            }
            else if (removePlayerCard != null)
            {
                playerCards.Remove(removePlayerCard);
            }

            _currencyService.AddCurrency(sellPrice);
            UnityEngine.Object.Destroy(cardView.gameObject);
        }

    }
}