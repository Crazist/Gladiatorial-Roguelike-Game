using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using UI.Elements;
using Infrastructure.Services.CardsServices;
using UI.Service;
using UI.Services;
using UI.Type;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PermaDeckWindow : WindowBase
    {
        [SerializeField] private Transform _cardsParent; 
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private RectTransform _sellArea;
        [SerializeField] private Button _continueBtn;
        
        private PersistentProgressService _persistentProgressService;
        private CardPopupService _cardPopupService;
        private WindowService _windowService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, CardPopupService cardPopupService,
            CardDragService cardDragService, WindowService windowService)
        {
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;
            
            SetSellArea(cardDragService);
        }

        private void Start()
        {
            RegisterBtns();
            CreateCards();
        }

        private void RegisterBtns() => 
            _continueBtn.onClick.AddListener(OnContinueClick);

        private void SetSellArea(CardDragService cardDragService) => 
            cardDragService.SetSellArea(_sellArea);

        private void CreateCards()
        {
            List<Card> cards = _persistentProgressService.PlayerProgress.DeckProgress.PermaDeck.Cards;
            
            foreach (Card card in cards)
            {
                CardView cardView = Instantiate(_cardPrefab, _cardsParent);
                cardView.Initialize(card.CardData, true);

                _cardPopupService.SubscribeToCard(cardView);
            }
        }

        private void OnContinueClick() => 
            _windowService.Open(WindowId.EnemyChoose);
    }
}
