using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure.States.BattleStates
{
    public class BattleEndState : IState
    {
        private GameStateMachine _gameStateMachine;
        private CardService _cardService;
        private TableService _tableService;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, CardService cardService, TableService tableService)
        {
            _tableService = tableService;
            _cardService = cardService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
             EndBattle();
        }

        public void Exit()
        {
            _cardService.CleanUp();
            _tableService.CleanUp();
        }

        private void EndBattle() => 
            _gameStateMachine.Enter<EndGameState>();
    }
}