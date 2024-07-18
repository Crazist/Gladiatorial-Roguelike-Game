using System.Collections.Generic;
using Logic.Entities;
using UI.Elements;
using Zenject;

namespace Infrastructure.Services
{
    public class TableService
    {
        private List<Card> _playerHand;
        private List<Card> _enemyHand;
        private List<Card> _playerTable;
        private List<Card> _enemyTable;
        private CardView _hoveredCard;

        private CardService _cardService;

        [Inject]
        private void Inject(CardService cardService)
        {
            _cardService = cardService;

            _playerHand = new List<Card>();
            _enemyHand = new List<Card>();
            _playerTable = new List<Card>();
            _enemyTable = new List<Card>();
        }

        public void InitializeHands()
        {
            _playerHand = _cardService.DrawPlayerHand(6);
            _enemyHand = _cardService.DrawEnemyHand(6);
        }

        public void AddCardToPlayerHand(Card card)
        {
            if (_playerHand.Count < 6)
                _playerHand.Add(card);
        }

        public void AddCardToEnemyHand(Card card)
        {
            if (_enemyHand.Count < 6)
                _enemyHand.Add(card);
        }

        public void DrawCardForPlayer()
        {
            int cardsNeeded = 6 - _playerHand.Count;
            if (cardsNeeded > 0)
            {
                var drawnCards = _cardService.DrawPlayerHand(cardsNeeded);
                foreach (var card in drawnCards)
                {
                    AddCardToPlayerHand(card);
                }
            }
        }

        public void DrawCardForEnemy()
        {
            int cardsNeeded = 6 - _enemyHand.Count;
            if (cardsNeeded > 0)
            {
                var drawnCards = _cardService.DrawEnemyHand(cardsNeeded);
                foreach (var card in drawnCards)
                {
                    AddCardToEnemyHand(card);
                }
            }
        }

        public void RemoveCardFromPlayerHand(Card card) => _playerHand.Remove(card);
        public void RemoveCardFromEnemyHand(Card card) => _enemyHand.Remove(card);
        public void AddCardToPlayerTable(Card card) => _playerTable.Add(card);
        public void AddCardToEnemyTable(Card card) => _enemyTable.Add(card);
        public void RemoveCardFromPlayerTable(Card card) => _playerTable.Remove(card);
        public void RemoveCardFromEnemyTable(Card card) => _enemyTable.Remove(card);
        public void SetHoveredCard(CardView card) => _hoveredCard = card;
        public List<Card> GetEnemyHand() => _enemyHand;
        public List<Card> GetPlayerTable() => _playerTable;
        public List<Card> GetPlayerHand() => _playerHand;
        public List<Card> GetEnemyTable() => _enemyTable;
        public CardView GetHoveredCard() => _hoveredCard;
    }
}