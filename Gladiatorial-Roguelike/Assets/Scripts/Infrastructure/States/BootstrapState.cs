using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            //Empty for now
        }

        public void Exit()
        {
        }
    }
}