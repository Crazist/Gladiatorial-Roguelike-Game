using Infrastructure.StateMachines;
using UI.Factory;
using Zenject;

namespace Infrastructure.States
{
    public class EndGameState : IState
    {
        private const string EndGameScene = "EndGameScene";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, SceneLoader sceneLoader, UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            _sceneLoader.Load(EndGameScene, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _uiFactory.CreateUiRoot();
        } 
        
        public void Exit()
        {
        }
    }
}