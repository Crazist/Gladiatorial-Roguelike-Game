using Infrastructure.Data;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.PersistentProgress;
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
        private PersistentProgressService _persistentProgressService;
        private SaveLoadService _saveLoadService;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIFactory uiFactory,
            WindowService windowService, BattleResultService battleResultService,
            PersistentProgressService persistentProgressService, SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _persistentProgressService = persistentProgressService;
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
                UpdateCurrentEnemyDeck();
                _saveLoadService.SaveProgress();
                _windowService.Open(WindowId.VictoryWindow);
                return;
            }

            _windowService.Open(WindowId.LoseWindow);
        }

        private void UpdateCurrentEnemyDeck()
        {
            switch (_persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.ChoosenDeck)
            {
                case DeckComplexity.Easy:
                    _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.EasyDeck.IsSkipped =
                        EnemyDeckState.Defeated;
                    break;
                case DeckComplexity.Intermediate:
                    _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.IntermediateDeck.IsSkipped =
                        EnemyDeckState.Defeated;
                    break;
                case DeckComplexity.Hard:
                    _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.HardDeck.IsSkipped =
                        EnemyDeckState.Defeated;
                    break;
            }
        }
    }
}