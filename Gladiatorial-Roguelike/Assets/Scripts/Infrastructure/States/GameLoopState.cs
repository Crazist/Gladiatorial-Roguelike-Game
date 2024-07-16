using Zenject;

namespace Infrastructure
{
    public class GameLoopState :  IState
    {
        private const string gameLoopScene = "GameLoopScene";
        
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Inject(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter() => 
            _sceneLoader.Load(gameLoopScene, OnLoadLevel);

        public void Exit()
        {
           
        }

        private void OnLoadLevel()
        {
            
        }
    }
}