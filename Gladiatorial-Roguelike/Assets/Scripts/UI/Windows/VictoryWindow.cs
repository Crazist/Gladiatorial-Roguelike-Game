using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using Logic.Entities;
using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class VictoryWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _viewDeckButton;
        [SerializeField] private RectTransform _lostCardsContainer;
        [SerializeField] private CardView _cardViewPrefab;

        [Inject]
        public void Inject(BattleResultService battleResultService)
        {
            InitializeBtns();
            DisplayLostCards(battleResultService.PlayerLost);
        }

        private void InitializeBtns()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _viewDeckButton.onClick.AddListener(OnViewDeckButtonClicked);
        }

        private void DisplayLostCards(List<Card> lostCards)
        {
            foreach (var card in lostCards)
            {
                var cardView = Instantiate(_cardViewPrefab, _lostCardsContainer);

                cardView.Initialize(card, TeamType.None, false);
                cardView.ChangeRaycasts(false);
            }
        }

        private void OnViewDeckButtonClicked()
        {
            // throw new System.NotImplementedException();
        }

        private void OnContinueButtonClicked()
        {
            // throw new System.NotImplementedException();
        }
    }
}