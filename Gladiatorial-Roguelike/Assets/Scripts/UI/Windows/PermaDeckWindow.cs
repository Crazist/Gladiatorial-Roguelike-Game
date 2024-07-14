using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using UI.Elements;
using Infrastructure.Services.CardsServices;
using UI.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PermaDeckWindow : WindowBase
    {
        [SerializeField] private Transform _cardsParent; 
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private RectTransform _sellArea;
        
        private PersistentProgressService _persistentProgressService;
        private CardPopupService _cardPopupService;
        
        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, CardPopupService cardPopupService,
            CardDragService cardDragService)
        {
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;
            
            SetSellArea(cardDragService);
        }

        private void Start() => 
            CreateCards();

        private void SetSellArea(CardDragService cardDragService) => 
            cardDragService.SetSellArea(_sellArea);

        private void CreateCards()
        {
            List<Card> cards = _persistentProgressService.PlayerProgress.Profile.PermaDeck.Cards;
            
            foreach (Card card in cards)
            {
                CardView cardView = Instantiate(_cardPrefab, _cardsParent);
                cardView.Initialize(card.CardData, true);

                _cardPopupService.SubscribeToCard(cardView);
            }
        }
    }
}
