using System;
using Zenject;

namespace Infrastructure
{
    public class PlayerTurnState : IState
    {
        private BattleStateMachine _battleStateMachine;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            PlayerTurn();
        }

        public void Exit()
        {
         
        }

        private void PlayerTurn()
        {
            _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}