using System;
using Infrastructure.Services.BattleService;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class EnemyTurnState : IState
    {
        private  BattleStateMachine _battleStateMachine;
        private TurnService _turnService;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine) => 
            _battleStateMachine = battleStateMachine;

        public void Enter()
        {
            EnemyTurn();
        }

        public void Exit()
        {
            
        }

        private void EnemyTurn()
        {
           //_battleStateMachine.Enter<PlayerTurnState>();
        }
    }
}