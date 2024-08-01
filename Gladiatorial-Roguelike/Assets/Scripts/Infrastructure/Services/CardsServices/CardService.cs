using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using Logic.Entities;
using Logic.Types;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class CardService
    {
        private List<Card> _shuffledPlayerDeck;
        private List<Card> _shuffledEnemyDeck;
       
        private PersistentProgressService _persistentProgressService;
        private PlayerDeckService _playerDeckService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, PlayerDeckService playerDeckService)
        {
            _playerDeckService = playerDeckService;
            _persistentProgressService = persistentProgressService;
        }

        public void ShuffleDecks()
        {
            _shuffledPlayerDeck = new List<Card>(_playerDeckService.GetDeck());
            _shuffledPlayerDeck = RemoveDeadCards();
            ShuffleDeck(_shuffledPlayerDeck);

            var enemyDeck = GetEnemyDeck();
            _shuffledEnemyDeck = new List<Card>(enemyDeck);
            ShuffleDeck(_shuffledEnemyDeck);
        }

        public List<Card> DrawPlayerHand(int count) =>
            DrawCards(_shuffledPlayerDeck, count);

        public List<Card> DrawEnemyHand(int count) =>
            DrawCards(_shuffledEnemyDeck, count);

        public int GetRemainingPlayerCardsCount() =>
            _shuffledPlayerDeck.Count;

        public int GetRemainingEnemyCardsCount() =>
            _shuffledEnemyDeck.Count;

        public int GetMaxPlayerCardsCount() =>
            _playerDeckService.GetDeck().Count;

        public int GetMaxEnemyCardsCount()
        {
            var enemyProgress = _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress;
            return enemyProgress.ChoosenDeck switch
            {
                DeckComplexity.Easy => enemyProgress.EasyDeck.Cards.Count,
                DeckComplexity.Intermediate => enemyProgress.IntermediateDeck.Cards.Count,
                DeckComplexity.Hard => enemyProgress.HardDeck.Cards.Count,
                _ => 0
            };
        }

        public void CleanUp()
        {
            _shuffledPlayerDeck.Clear();
            _shuffledEnemyDeck.Clear();
        }

        private List<Card> RemoveDeadCards() => 
            _shuffledPlayerDeck.Where(card => !card.IsDead).ToList();

        private void ShuffleDeck(List<Card> deck)
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }
        }

        private List<Card> DrawCards(List<Card> deck, int count)
        {
            var drawnCards = new List<Card>();

            for (int i = 0; i < count && deck.Count > 0; i++)
            {
                drawnCards.Add(deck[0]);
                deck.RemoveAt(0);
            }

            return drawnCards;
        }

        private List<Card> GetEnemyDeck()
        {
            var enemyProgress = _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress;
            return enemyProgress.ChoosenDeck switch
            {
                DeckComplexity.Easy => enemyProgress.EasyDeck.Cards,
                DeckComplexity.Intermediate => enemyProgress.IntermediateDeck.Cards,
                DeckComplexity.Hard => enemyProgress.HardDeck.Cards,
                _ => new List<Card>()
            };
        }
    }
}
