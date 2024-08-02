using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using Logic.Enteties;
using Logic.Types;
using TMPro;
using UI.Service;
using UI.Type;
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
        [SerializeField] private TMP_Text _currencyRewardText;
        [SerializeField] private RectTransform _lostCardsContainer;
        [SerializeField] private RectTransform _rewardedCardsContainer;
        [SerializeField] private CardView _cardViewPrefab;

        private DeckViewModel _deckViewModel;
        private WindowService _windowService;

        [Inject]
        public void Inject(BattleResultService battleResultService, DeckViewModel deckViewModel,
            WindowService windowService)
        {
            _windowService = windowService;
            _deckViewModel = deckViewModel;

            InitializeBtns();
            DisplayLostCards(battleResultService.PlayerLost);
            DisplayCurrencyText(battleResultService.CurrencyReward);
            DisplayRewardedCard(battleResultService.RewardedCard);
        }

        private void DisplayCurrencyText(int count) =>
            _currencyRewardText.text = count + " Gold";

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

        private void DisplayRewardedCard(Card rewardedCard)
        {
            if (rewardedCard != null)
            {
                var cardView = Instantiate(_cardViewPrefab, _rewardedCardsContainer);
                cardView.Initialize(rewardedCard, TeamType.None, false);
                cardView.ChangeRaycasts(false);
            }
        }

        private void OnViewDeckButtonClicked()
        {
            _deckViewModel.SetContinueBtn(false);
            _windowService.Open(WindowId.DeckWindow);
        }

        private void OnContinueButtonClicked() => _windowService.Open(WindowId.BlackSmithWindow);
    }
}
