using System.Collections.Generic;
using Logic.Entities;
using UI.Elements;
using Zenject;

namespace Infrastructure.Services
{
    public class TableService
    {
        private List<CardView> _playerHandViews;
        private List<CardView> _enemyHandViews;
        private List<CardView> _playerTableViews;
        private List<CardView> _enemyTableViews;

        private CardService _cardService;

        private CardView _hoveredCard;

        [Inject]
        private void Inject(CardService cardService)
        {
            _cardService = cardService;
            _playerHandViews = new List<CardView>();
            _enemyHandViews = new List<CardView>();
            _playerTableViews = new List<CardView>();
            _enemyTableViews = new List<CardView>();
        }

        public List<Card> DrawEnemyHand() => _cardService.DrawEnemyHand(6);
        public List<Card> DrawPlayerHand() => _cardService.DrawPlayerHand(6);
        
        public List<CardView> GetPlayerHandViews() => _playerHandViews;
        public List<CardView> GetEnemyHandViews() => _enemyHandViews;
        public List<CardView> GetPlayerTableViews() => _playerTableViews;
        public List<CardView> GetEnemyTableViews() => _enemyTableViews;
        public CardView GetHoveredCard() => _hoveredCard;

        public void SetHoveredCard(CardView card) => _hoveredCard = card;
    }
}