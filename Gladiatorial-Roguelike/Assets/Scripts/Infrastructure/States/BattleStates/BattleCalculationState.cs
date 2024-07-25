using System.Collections;
using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using UI.Factory;
using Zenject;

namespace Infrastructure.States.BattleStates
{
    public class BattleCalculationState : IState
    {
        private readonly BattleStateMachine _battleStateMachine;
        private readonly BattleService _battleService;
        private readonly TurnService _turnService;
        private readonly CoroutineCustomRunner _coroutineCustomRunner;
        private readonly UIFactory _uiFactory;
        private readonly AttackService _attackService;
        private readonly TableService _tableService;

        [Inject]
        public BattleCalculationState(BattleStateMachine battleStateMachine, BattleService battleService,
            TurnService turnService, CoroutineCustomRunner coroutineCustomRunner, UIFactory uiFactory,
            AttackService attackService,
            TableService tableService)
        {
            _tableService = tableService;
            _attackService = attackService;
            _uiFactory = uiFactory;
            _coroutineCustomRunner = coroutineCustomRunner;
            _battleStateMachine = battleStateMachine;
            _battleService = battleService;
            _turnService = turnService;
        }

        public void Enter()
        {
            _uiFactory.TurnShower.SetTurnText(StringHelper.Calculation);
            _turnService.StartNonPlayerInteractionStateStart();
            _attackService.DeactivateAllArrows();
            _coroutineCustomRunner.StartCoroutine(ProcessAttacks());
        }

        public void Exit()
        {
        }

        private IEnumerator ProcessAttacks()
        {
            yield return _battleService.ExecuteBotActions();
            yield return _battleService.CalculateAttacks();

            _turnService.StartTurnEnd();

            if (!CheckIfEndBattle())
            {
                _battleStateMachine.Enter<EnemyTurnState>();
            }
        }

        private bool CheckIfEndBattle()
        {
            if ((_tableService.GetEnemyTableViews().Count == 0 && _tableService.DrawEnemyHand().Count == 0) ||
                (_tableService.GetPlayerTableViews().Count == 0 && _tableService.DrawPlayerHand().Count == 0))
            {
                _turnService.StartEndBattle();
                return true;
            }

            return false;
        }
    }
}