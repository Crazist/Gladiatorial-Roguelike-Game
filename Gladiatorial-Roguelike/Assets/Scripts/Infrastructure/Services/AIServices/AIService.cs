using System.Collections;
using System.Collections.Generic;
using Infrastructure.StateMachines;
using Logic.Entities;
using Logic.Types;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class AIService
    {
        private List<EnemyCardDropArea> _enemyDropAreas;
        
        private BattleStateMachine _battleStateMachine;
        private TableService _tableService;
        private CoroutineCustomRunner _coroutineRunner;
        private System.Random _random;

        [Inject]
        public void Inject(BattleStateMachine battleStateMachine, TableService tableService, CoroutineCustomRunner coroutineRunner)
        {
            _battleStateMachine = battleStateMachine;
            _tableService = tableService;
            _coroutineRunner = coroutineRunner;
            _random = new System.Random();
        }

        public void Initialize(List<EnemyCardDropArea> enemyDropAreas) => 
            _enemyDropAreas = enemyDropAreas;

        public void ExecuteEnemyTurn() => 
            _coroutineRunner.StartCoroutine(ExecuteTurnRoutine());

        private IEnumerator ExecuteTurnRoutine()
        {
            yield return new WaitForSeconds(1);

            yield return PlayEnemyCardsWithDelay();

            yield return new WaitForSeconds(1);

            _battleStateMachine.Enter<PlayerTurnState>();
        }

        private IEnumerator PlayEnemyCardsWithDelay()
        {
            var enemyHand = _tableService.GetEnemyHand();
            var cardsToPlay = new List<Card>();

            foreach (var cardToPlay in enemyHand)
            {
                if (cardToPlay.CardData.Category == CardCategory.Unit && _random.NextDouble() < 0.5)
                {
                    var availableDropArea = GetAvailableDropArea();
                    
                    if (availableDropArea != null)
                    {
                        cardsToPlay.Add(cardToPlay);
                        yield return PlaceCardInDropAreaWithDelay(cardToPlay, availableDropArea);
                    }
                }
            }

            if (cardsToPlay.Count == 0)
            {
                var cardToPlay = TryPlayAtLeastOneCard();
                if (cardToPlay != null)
                {
                    var availableDropArea = GetAvailableDropArea();
                    if (availableDropArea != null)
                    {
                        _tableService.RemoveCardFromEnemyHand(cardToPlay);
                        _tableService.AddCardToEnemyTable(cardToPlay);
                        yield return PlaceCardInDropAreaWithDelay(cardToPlay, availableDropArea);
                    }
                }
            }
        }

        private Card TryPlayAtLeastOneCard()
        {
            var enemyHand = _tableService.GetEnemyHand();
            foreach (var cardToPlay in enemyHand)
            {
                if (cardToPlay.CardData.Category == CardCategory.Unit)
                {
                    return cardToPlay;
                }
            }
            return null;
        }

        private IEnumerator PlaceCardInDropAreaWithDelay(Card cardToPlay, EnemyCardDropArea dropArea)
        {
            var enemyCardViews = _tableService.GetEnemyCardViews();
            var cardView = enemyCardViews.Find(cv => cv.GetCard() == cardToPlay);
            if (cardView != null)
            {
                cardView.ChangeRaycasts(false);
                cardView.GetCardDisplay().FlipCard();
                dropArea.HandleDrop(cardView, null);
               
                yield return new WaitForSeconds(1);
            }
        }

        private EnemyCardDropArea GetAvailableDropArea()
        {
            foreach (var dropArea in _enemyDropAreas)
            {
                if (!dropArea.IsOccupied())
                {
                    return dropArea;
                }
            }
            return null;
        }
    }
}
