using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.CardsServices;
using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using UI.Factory;
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

        public override void InstallBindings()
        {
            UIBinds();

            Container.Bind<CoroutineCustomRunner>().FromComponentInNewPrefab(_coroutineCustomRunner).AsSingle().NonLazy();
            
            Container.Bind<Game>().AsSingle().NonLazy();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
            Container.Bind<Factory>().AsSingle().NonLazy();
            Container.Bind<DeckService>().AsSingle().NonLazy();
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
          
            Container.Bind<DeckViewModel>().AsSingle().NonLazy();
        }

        private void UIBinds()
        {
            var canvasInstance = Container.InstantiatePrefabForComponent<Canvas>(_canvasPrefab);

            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_curtain)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
            Container.Bind<CardPopup>().FromComponentInNewPrefab(_cardPopup)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
            Container.Bind<ConfirmationPopup>().FromComponentInNewPrefab(_confirmPopup)
                .UnderTransform(canvasInstance.transform).AsSingle().NonLazy();
        }
    }
}