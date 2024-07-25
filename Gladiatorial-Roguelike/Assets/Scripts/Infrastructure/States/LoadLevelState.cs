using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Logic.Cards;
using UI.Factory;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        private UIFactory _uiFactory;
        private EnemyService _enemyService;
        
        [Inject]
        private void Inject(GameStateMachine stateMachine, PlayerDeckService playerDeckService, SceneLoader sceneLoader,
            LoadingCurtain curtain, UIFactory uiFactory, EnemyService enemyService)
        {
            _enemyService = enemyService;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _enemyService.InitEnemyDecks();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            _uiFactory.CreateUiRoot();
            _uiFactory.CreateMenu();
            _uiFactory.CreateDebugPanel();

            _stateMachine.Enter<MenuState>();
        }
    }
}