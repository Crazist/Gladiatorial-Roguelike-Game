using UI.Services;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data.Cards;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Logic.Types;
using UI.Elements.CardDrops;
using Zenject;

namespace UI.Windows
{
    public class BlacksmithWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private RectTransform _deckViewContent;
        [SerializeField] private CardView _cardPrefab;

        [SerializeField] private HealCardDropArea _healArea;
        [SerializeField] private UpgradeCardDropArea _upgradeArea;
        [SerializeField] private SellArea _saleArea;

        private List<CardView> _cardViews = new List<CardView>();
        private TableService _tableService;
        private CardDragService _cardDragService;

        [Inject]
        private void Inject(TableService tableService, CardDragService cardDragService)
        {
            _cardDragService = cardDragService;
            _tableService = tableService;

            InitializeBtns();
            InitializeDropAreas();
            SpawnCards();
        }
        
        private void InitializeBtns() => 
            _continueButton.onClick.AddListener(OnContinueButtonClicked);

        private void SpawnCards()
        {
            foreach (var cardData in _tableService.DrawPlayerHand())
            {
                CardView cardView = Instantiate(_cardPrefab, _deckViewContent);
                cardView.Initialize(cardData, TeamType.None, true);
                _cardViews.Add(cardView);
            }
        }
        private void InitializeDropAreas()
        {
            _cardDragService.AddDropArea(_healArea);
            _cardDragService.AddDropArea(_upgradeArea);
            _cardDragService.AddDropArea(_saleArea);
        }
        private void OnContinueButtonClicked() => 
            Debug.Log("Continue to next level");
    }
}