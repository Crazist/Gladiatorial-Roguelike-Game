using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
{  //[SerializeField] private LoadingCurtain _curtain;
    public override void InstallBindings()
    {  
       // Container.BindInstance(_curtain).AsSingle();
       // Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_curtain).AsSingle().NonLazy();
        Container.Bind<Factory>().AsSingle().NonLazy();
        Container.Bind<DeckService>().AsSingle().NonLazy();
        Container.Bind<StaticDataService>().AsSingle().NonLazy();
    }
}