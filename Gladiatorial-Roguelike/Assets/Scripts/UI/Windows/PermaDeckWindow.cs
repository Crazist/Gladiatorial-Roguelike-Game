using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using Infrastructure.Services.CardsServices;
using Logic.Enteties;
using Logic.Entities;
using Logic.Types;
using UI.Elements.CardDrops;
using UI.Model;
using UI.Services;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PermaDeckWindow : WindowBase
    {
        [SerializeField] private Transform _cardsParent;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private CardDropArea _sellArea;
        [SerializeField] private Button _continueBtn;

        private PersistentProgressService _persistentProgressService;
        private CardPopupService _cardPopupService;
        private CardDragService _cardDragService;

        private bool _hasContinueBtn;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, CardPopupService cardPopupService,
            CardDragService cardDragService, PermaDeckModel permaDeckModel)
        {
            _persistentProgressService = persistentProgressService;
            _cardPopupService = cardPopupService;
            _cardDragService = cardDragService;

            _hasContinueBtn = permaDeckModel.HasContinueBtn;

            SetDropAreas();
        }

        private void Start()
        {
            ActivateContinueBtn();
            CreateCards();
        }

        private void ActivateContinueBtn() =>
            _continueBtn.gameObject.SetActive(_hasContinueBtn);

        private void SetDropAreas() => 
            _cardDragService.AddDropArea(_sellArea);

        private void CreateCards()
        {
            List<Card> cards = _persistentProgressService.PlayerProgress.PermaDeck.Cards;

            foreach (Card card in cards)
            {
                CardView cardView = Instantiate(_cardPrefab, _cardsParent);
                cardView.Initialize(card, TeamType.Ally, true);

                _cardPopupService.SubscribeToCard(cardView);
            }
        }
    }
}
