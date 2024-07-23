using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using UI.Factory;
using Zenject;

namespace Infrastructure
{
    public class PlayerTurnState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private TurnService _turnService;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
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
            _uiFactory.TurnShower.SetTurnText(StringHelper.PlayerTurn);
        //    _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}