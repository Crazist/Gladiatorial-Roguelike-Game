using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI.Elements;
using UI.Factory;
using UI.Service;
using UnityEngine;
using Zenject;

namespace Data.DI
{
    [CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
    public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
    {
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private CardPopup _cardPopup;
        [SerializeField] private CoroutineCustomRunner _coroutineCustomRunner;

        public override void InstallBindings()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_curtain).AsSingle().NonLazy();
            Container.Bind<CardPopup>().FromComponentInNewPrefab(_cardPopup).AsSingle().NonLazy();
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
          
            Container.Bind<DeckViewModel>().AsSingle().NonLazy();
        }
    }
}