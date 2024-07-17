using System;
using Zenject;

namespace Infrastructure
{
    public class EnemyTurnState : IState
    {
        private  BattleStateMachine _battleStateMachine;
        
        [Inject]
        private void Inject(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            EnemyTurn();
        }

        public void Exit()
        {
            
        }

        private void EnemyTurn()
        {
           // _aiController.MakeMove();
         //   _battleStateMachine.Enter<PlayerTurnState>();
        }
    }
}