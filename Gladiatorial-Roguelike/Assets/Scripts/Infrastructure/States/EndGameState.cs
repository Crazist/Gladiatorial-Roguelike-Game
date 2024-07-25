using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using Logic.Types;
using UI.Factory;
using UI.Service;
using UI.Type;
using Zenject;

namespace Infrastructure.States
{
    public class EndGameState : IState
    {
        private const string EndGameScene = "EndGameScene";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private UIFactory _uiFactory;
        private WindowService _windowService;
        private BattleResultService _battleResultService;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIFactory uiFactory,
            WindowService windowService, BattleResultService battleResultService)
        {
            _battleResultService = battleResultService;
            _windowService = windowService;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _sceneLoader.Load(EndGameScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _uiFactory.CreateUiRoot();
            OpenWinLoseWindow();
        }

        private void OpenWinLoseWindow()
        {
            if (_battleResultService.BattleResult == BattleResult.Win)
            {
                _windowService.Open(WindowId.VictoryWindow);
            }
        }
    }
}