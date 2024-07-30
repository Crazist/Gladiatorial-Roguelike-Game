using Infrastructure.Services;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Infrastructure.StateMachines;
using UI.Factory;
using UI.Service;
using UI.Type;
using Zenject;

namespace Infrastructure.States.BattleStates
{
    public class BattleStartState : IState
    {
        private BattleStateMachine _battleStateMachine;
        private CardService _cardService;
        private TableService _tableService;
        private WindowService _windowService;
        private TurnService _turnService;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine, CardService cardService, TableService tableService
        ,WindowService windowService, UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
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
            
            _windowService.Open(WindowId.TableWindow);
            _uiFactory.CreateTurnIndicator();
            _uiFactory.CreateDebugPanel();
            
            _battleStateMachine.Enter<EnemyTurnState>();
        }
    }
}