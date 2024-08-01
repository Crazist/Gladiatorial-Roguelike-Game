using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Infrastructure.StateMachines;
using Infrastructure.Services.Currency;
using Zenject;

namespace Infrastructure.States.BattleStates
{
    public class BattleEndState : IState
    {
        private GameStateMachine _gameStateMachine;
        private CardService _cardService;
        private TableService _tableService;
        private CardDragService _cardDragService;
        private RewardService _rewardService;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, CardService cardService, TableService tableService,
            CardDragService cardDragService, RewardService rewardService)
        {
            _cardDragService = cardDragService;
            _tableService = tableService;
            _cardService = cardService;
            _gameStateMachine = gameStateMachine;
            _rewardService = rewardService;
        }

        public void Enter() => EndBattle();

        public void Exit()
        {
            _cardService.CleanUp();
            _tableService.CleanUp();
            _cardDragService.CleanUp();
        }

        private void EndBattle()
        {
            _rewardService.GrantReward();
            _gameStateMachine.Enter<EndGameState>();
        }
    }
}