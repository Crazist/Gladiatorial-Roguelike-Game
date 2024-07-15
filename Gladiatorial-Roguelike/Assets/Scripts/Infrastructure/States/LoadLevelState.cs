using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
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
        private DeckService _decService;
        private UIFactory _uiFactory;
        private StaticDataService _staticDataService;
        private PermaDeckService _permaDeckService;
        private Factory _factory;
        private EnemyService _enemyService;
       
        [Inject]
        private void Inject(GameStateMachine stateMachine, DeckService deckService, SceneLoader sceneLoader,
            LoadingCurtain curtain, UIFactory uiFactory, StaticDataService staticDataService,
            PermaDeckService permaDeckService, Factory factory, EnemyService enemyService)
        {
            _enemyService = enemyService;
            _factory = factory;
            _permaDeckService = permaDeckService;
            _staticDataService = staticDataService;
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

        private void CreateDeck()
        {
            if (_permaDeckService.GetAllCards().Count > 0) return;

            DeckData deck = _staticDataService.ForDeck(DeckType.RomanDeck);

            _permaDeckService.AddCardToDeck(_factory.CreateCard(deck.Cards[0]));
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUiRoot();
            _uiFactory.CreateMenu();
            _uiFactory.CreateDebugPanel();
           
            CreateDeck();

            _enemyService.InitEnemyDecks();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}