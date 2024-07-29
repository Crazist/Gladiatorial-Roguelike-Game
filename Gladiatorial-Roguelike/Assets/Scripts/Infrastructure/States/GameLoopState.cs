using Infrastructure.StateMachines;
using Infrastructure.States.BattleStates;
using UI.Factory;
using Zenject;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private const string gameLoopScene = "GameLoopScene";
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private UIFactory _uiFactory;
        private BattleStateMachine _battleStateMachine;

        [Inject]
        private void Inject(SceneLoader sceneLoader, GameStateMachine gameStateMachine,
            UIFactory uiFactory, BattleStateMachine battleStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            _sceneLoader.Load(gameLoopScene, OnLoadLevel);
        }

        public void Exit()
        {
        }

        private void OnLoadLevel()
        {
            _uiFactory.CreateUiRoot();
            _battleStateMachine.Enter<BattleStartState>();
        }
    }
}