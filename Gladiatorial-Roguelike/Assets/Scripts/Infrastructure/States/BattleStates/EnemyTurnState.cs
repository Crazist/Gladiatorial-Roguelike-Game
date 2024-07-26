using System.Collections;
using Infrastructure.Services.AIServices;
using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using UI.Factory;
using Zenject;

namespace Infrastructure.States.BattleStates
{
    public class EnemyTurnState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private AIService _aiService;
        private AIBuffService _aiBuffService;
        private CoroutineCustomRunner _coroutineRunner;
        private UIFactory _uiFactory;
        private BattleService _battleService;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, AIService aiService, AIBuffService aiBuffService,
            CoroutineCustomRunner coroutineRunner, UIFactory uiFactory, BattleService battleService)
        {
            _battleService = battleService;
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
            yield return _battleService.ExecuteBotDefense();

            _battleStateMachine.Enter<PlayerTurnState>();
        }
    }
}