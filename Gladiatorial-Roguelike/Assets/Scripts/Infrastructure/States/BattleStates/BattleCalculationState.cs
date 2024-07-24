using System.Collections;
using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using UI.Factory;
using UnityEngine;
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
        private AttackService _attackService;

        [Inject]
        public BattleCalculationState(BattleStateMachine battleStateMachine, BattleService battleService,
            TurnService turnService, CoroutineCustomRunner coroutineCustomRunner, UIFactory uiFactory, AttackService attackService)
        {
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
            yield return _battleService.CalculateAttacks();
            yield return _battleService.ExecuteBotAttacks();

            _turnService.StartTurnEnd();
            
            _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}