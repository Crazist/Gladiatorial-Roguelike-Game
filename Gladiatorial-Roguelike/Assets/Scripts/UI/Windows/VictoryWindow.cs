using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using Logic.Enteties;
using Logic.Types;
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
        [SerializeField] private RectTransform _lostCardsContainer;
        [SerializeField] private CardView _cardViewPrefab;
        
        private DeckViewModel _deckViewModel;
        private WindowService _windowService;

        [Inject]
        public void Inject(BattleResultService battleResultService, DeckViewModel deckViewModel, WindowService windowService)
        {
            _windowService = windowService;
            _deckViewModel = deckViewModel;
            
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
            _deckViewModel.SetContinueBtn(false);
            _windowService.Open(WindowId.DeckWindow);
        }

        private void OnContinueButtonClicked() => _windowService.Open(WindowId.BlackSmithWindow);
    }
}