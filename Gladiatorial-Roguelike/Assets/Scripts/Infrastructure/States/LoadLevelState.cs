using Infrastructure.Services;
using UI.Factory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class  LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly DeckService _decService;
        private readonly UIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine,DeckService deckService, SceneLoader sceneLoader,
            LoadingCurtain curtain, UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _decService = deckService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _decService.InitializeDeck();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _curtain.Hide();

        private void OnLoaded()
        {
            _uiFactory.CreateUiRoot();
            _uiFactory.CreateMenu();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}