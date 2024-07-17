using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Cards;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Windows
{
    public class TableWindow: WindowBase
    {
        [SerializeField] private Transform _playerHandArea;
        [SerializeField] private Transform _enemyHandArea;
        [SerializeField] private CardView _cardPrefab;

        private CardService _cardService;
        private TableService _tableService;

        [Inject]
        private void Inject(CardService cardService, TableService tableService)
        {
            _tableService = tableService;
            _cardService = cardService;

            InitializePlayerHand();
            InitializeEnemyHand();
        }

        public void InitializePlayerHand()
        {
            foreach (var card in _tableService.GetPlayerHand())
            {
                var cardView = Instantiate(_cardPrefab, _playerHandArea);
                cardView.Initialize(card.CardData, true);
            }
        }

        public void InitializeEnemyHand()
        {
            foreach (var card in _tableService.GetEnemyHand())
            {
                var cardView = Instantiate(_cardPrefab, _enemyHandArea);
                cardView.Initialize(card.CardData, false);
            }
        }
    }
}