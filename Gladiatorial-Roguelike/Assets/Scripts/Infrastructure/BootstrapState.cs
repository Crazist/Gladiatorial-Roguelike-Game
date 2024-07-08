namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private readonly GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
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
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
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