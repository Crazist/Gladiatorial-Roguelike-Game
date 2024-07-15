using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using UI.Elements;
using Infrastructure.Services.CardsServices;
using UI.Model;
using UI.Services;
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

        private bool _hasContinueBtn;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, CardPopupService cardPopupService,
            CardDragService cardDragService, PermaDeckModel permaDeckModel)
        {
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;

            _hasContinueBtn = permaDeckModel.HasContinueBtn;

            SetSellArea(cardDragService);
        }

        private void Start()
        {
            ActivateContinueBtn();
            CreateCards();
        }

        private void ActivateContinueBtn() =>
            _continueBtn.gameObject.SetActive(_hasContinueBtn);

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
    }
}