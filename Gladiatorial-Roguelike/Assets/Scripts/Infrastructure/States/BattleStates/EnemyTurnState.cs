using Infrastructure.Services;
using Infrastructure.Services.BattleService;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class EnemyTurnState : IState
    {
        private  BattleStateMachine _battleStateMachine;
        private TurnService _turnService;
        private AIService _aiService;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, AIService aiService)
        {
            _aiService = aiService;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            EnemyTurn();
        }

        public void Exit()
        {
            
        }

        private void EnemyTurn() => 
            _aiService.ExecuteEnemyTurn();
    }
}