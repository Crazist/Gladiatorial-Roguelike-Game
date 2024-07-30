using Infrastructure.Services;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure.States
{
    public class MenuState : IState
    {
        private GameStateMachine _gameStateMachine;
        private EnemyService _enemyService;

        [Inject]
        private void Inject(SceneLoader sceneLoader, GameStateMachine gameStateMachine, EnemyService enemyService)
        {
            _enemyService = enemyService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            if(_enemyService.NeedRefreshEnemy())
                _enemyService.RefreshEnemy();
        }

        public void Exit()
        {
           
        }
    }
}