using System;
using Infrastructure.Services;
using UI.Service;
using UI.Type;
using Zenject;

namespace Infrastructure
{
    public class BattleStartState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private CardService _cardService;
        private TableService _tableService;
        private WindowService _windowService;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, CardService cardService, TableService tableService
        , WindowService windowService)
        {
            _windowService = windowService;
            _tableService = tableService;
            _cardService = cardService;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            StartBattle();
        }

        public void Exit()
        {
           
        }

        private void StartBattle()
        {
            _cardService.ShuffleDecks();
            _tableService.InitializeHands();
            
            _windowService.Open(WindowId.TableWindow);
            
            _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}