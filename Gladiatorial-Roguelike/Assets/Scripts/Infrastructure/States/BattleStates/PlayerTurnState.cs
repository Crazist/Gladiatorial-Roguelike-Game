using System;
using Infrastructure.Services.BattleService;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class PlayerTurnState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private TurnService _turnService;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine) => 
            _battleStateMachine = battleStateMachine;

        public void Enter()
        {
            PlayerTurn();
        }

        public void Exit()
        {
         
        }

        private void PlayerTurn()
        {
        //    _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}