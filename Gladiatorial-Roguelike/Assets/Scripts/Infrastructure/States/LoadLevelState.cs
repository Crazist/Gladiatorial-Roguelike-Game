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
        private PermaDeckService _permaDeckService;
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(GameStateMachine stateMachine, PlayerDeckService playerDeckService, SceneLoader sceneLoader,
            LoadingCurtain curtain, UIFactory uiFactory, EnemyService enemyService, PermaDeckService permaDeckService, PersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
            _permaDeckService = permaDeckService;
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
            _permaDeckService.AddCardToDeck(_persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.EasyDeck.Cards[0]);
            _permaDeckService.AddCardToDeck(_persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.EasyDeck.Cards[1]);
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