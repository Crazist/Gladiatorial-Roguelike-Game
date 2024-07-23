using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.AIServices;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.BuffsService;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Services;
using UI.Elements;
using UI.Factory;
using UI.Model;
using UI.Service;
using UI.Services;
using UnityEngine;
using Zenject;

namespace Data.DI
{
    [CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
    public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
    {
        [SerializeField] private GameObject _canvasPrefab;
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private CardPopup _cardPopup;
        [SerializeField] private CoroutineCustomRunner _coroutineCustomRunner;
        [SerializeField] private ConfirmationPopup _confirmPopup;
        [SerializeField] private DeckPopup _deckPopup;

        public override void InstallBindings()
        {
            UIBinds();
            RegisterModels();

            Container.Bind<CoroutineCustomRunner>().FromComponentInNewPrefab(_coroutineCustomRunner).AsSingle().NonLazy();

            Container.Bind<Game>().AsSingle().NonLazy();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
            Container.Bind<BattleStateMachine>().AsSingle().NonLazy();
            Container.Bind<Factory>().AsSingle().NonLazy();
            Container.Bind<PlayerDeckService>().AsSingle().NonLazy();
            Container.Bind<StaticDataService>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<WindowService>().AsSingle().NonLazy();
            Container.Bind<UIFactory>().AsSingle().NonLazy();
            Container.Bind<AssetProvider>().AsSingle().NonLazy();
            Container.Bind<PersistentProgressService>().AsSingle().NonLazy();
            Container.Bind<SaveLoadService>().AsSingle().NonLazy();
            Container.Bind<PermaDeckService>().AsSingle().NonLazy();
            Container.Bind<CardSellService>().AsSingle().NonLazy();
            Container.Bind<CardDragService>().AsSingle().NonLazy();
            Container.Bind<CardPopupService>().AsSingle().NonLazy();
            Container.Bind<EnemyService>().AsSingle().NonLazy();
            Container.Bind<CardSortingService>().AsSingle().NonLazy();
            Container.Bind<CardService>().AsSingle().NonLazy();
            Container.Bind<TableService>().AsSingle().NonLazy();
            Container.Bind<CardBuffService>().AsSingle().NonLazy();
            Container.Bind<BuffService>().AsSingle().NonLazy();
            Container.Bind<TurnService>().AsSingle().NonLazy();
            Container.Bind<AIService>().AsSingle().NonLazy();
            Container.Bind<AIBuffService>().AsSingle().NonLazy();
            Container.Bind<BuffProcessingService>().AsSingle().NonLazy();
            Container.Bind<BattleService>().AsSingle().NonLazy();
            Container.Bind<AttackService>().AsSingle().NonLazy();
            Container.Bind<CanvasService>().AsSingle().NonLazy();
            Container.Bind<DamageService>().AsSingle().NonLazy();
        }

        private void UIBinds()
        {
            var canvasInstance = Container.InstantiatePrefabForComponent<Canvas>(_canvasPrefab);

            Container.Bind<Canvas>().FromInstance(canvasInstance).AsSingle();
            
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_curtain)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
            Container.Bind<CardPopup>().FromComponentInNewPrefab(_cardPopup)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
            Container.Bind<ConfirmationPopup>().FromComponentInNewPrefab(_confirmPopup)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
            Container.Bind<DeckPopup>().FromComponentInNewPrefab(_deckPopup)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
        }

        private void RegisterModels()
        {
            Container.Bind<DeckViewModel>().AsSingle().NonLazy();
            Container.Bind<PermaDeckModel>().AsSingle().NonLazy();
        }
    }
}