using System.Collections;
using System.Collections.Generic;
using Infrastructure.StateMachines;
using Logic.Entities;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class AIService
    {
        private BattleStateMachine _battleStateMachine;
        private TableService _tableService;
        private CoroutineCustomRunner _coroutineRunner;
        private List<EnemyCardDropArea> _enemyDropAreas;

        [Inject]
        public void Inject(BattleStateMachine battleStateMachine, TableService tableService, CoroutineCustomRunner coroutineRunner)
        {
            _battleStateMachine = battleStateMachine;
            _tableService = tableService;
            _coroutineRunner = coroutineRunner;
        }

        public void Initialize(List<EnemyCardDropArea> enemyDropAreas)
        {
            _enemyDropAreas = enemyDropAreas;
        }

        public void ExecuteEnemyTurn()
        {
            _coroutineRunner.StartCoroutine(ExecuteTurnRoutine());
        }

        private IEnumerator ExecuteTurnRoutine()
        {
            yield return new WaitForSeconds(1); // задержка для имитации принятия решения

            PlayEnemyCards();

            yield return new WaitForSeconds(1); // задержка для имитации времени на выполнение действий

            // Завершение хода врага и передача хода игроку
            _battleStateMachine.Enter<PlayerTurnState>();
        }

        private void PlayEnemyCards()
        {
            var enemyHand = _tableService.GetEnemyHand();
            int availableZones = _enemyDropAreas.Count;

            for (int i = 0; i < availableZones && i < enemyHand.Count; i++)
            {
                var cardToPlay = enemyHand[i];
                _tableService.RemoveCardFromEnemyHand(cardToPlay);
                _tableService.AddCardToEnemyTable(cardToPlay);

                // Выбираем случайную зону для размещения карты
                var randomDropArea = _enemyDropAreas[Random.Range(0, _enemyDropAreas.Count)];
                PlaceCardInDropArea(cardToPlay, randomDropArea);
            }
        }

        private void PlaceCardInDropArea(Card cardToPlay, EnemyCardDropArea dropArea)
        {
            var enemyCardViews = _tableService.GetEnemyCardViews();
            var cardView = enemyCardViews.Find(cv => cv.GetCard() == cardToPlay);
            if (cardView != null)
            {
                cardView.ChangeRaycasts(false);
                dropArea.HandleDrop(cardView, null);
            }
        }
    }
}
