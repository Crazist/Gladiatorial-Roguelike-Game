using Infrastructure.Services;
using Infrastructure.Services.AIServices;
using Infrastructure.StateMachines;
using System.Collections;
using UI.Factory;
using Zenject;

namespace Infrastructure
{
    public class EnemyTurnState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private AIService _aiService;
        private AIBuffService _aiBuffService;
        private CoroutineCustomRunner _coroutineRunner;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, AIService aiService, AIBuffService aiBuffService,
            CoroutineCustomRunner coroutineRunner, UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _aiBuffService = aiBuffService;
            _aiService = aiService;
            _battleStateMachine = battleStateMachine;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _uiFactory.TurnShower.SetTurnText(StringHelper.EnemyTurn);
            
            _coroutineRunner.StartCoroutine(EnemyTurnRoutine());
        }

        public void Exit()
        {
        }

        private IEnumerator EnemyTurnRoutine()
        {
            yield return _aiService.ExecuteEnemyTurnRoutine();
            yield return _aiBuffService.ExecuteBuffsRoutine();

            _battleStateMachine.Enter<PlayerTurnState>();
        }
    }
}