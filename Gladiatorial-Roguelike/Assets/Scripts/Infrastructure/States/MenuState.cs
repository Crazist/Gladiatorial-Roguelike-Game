using Zenject;

namespace Infrastructure
{
    public class MenuState : IState
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Inject(SceneLoader sceneLoader, GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter()
        {
          
        }

        public void Exit()
        {
           
        }

        private void OnLoadLevel()
        {
            
        }
    }
}