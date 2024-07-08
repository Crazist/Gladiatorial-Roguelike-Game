using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameplayInstaller", menuName = "Installers/GameplayInstaller")]
public class GameplayInstaller : ScriptableObjectInstaller<GameplayInstaller>
{  //[SerializeField] private LoadingCurtain _curtain;
    public override void InstallBindings()
    {  
       // Container.BindInstance(_curtain).AsSingle();
       // Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_curtain).AsSingle().NonLazy();
       // Container.Bind<UserData>().AsSingle().NonLazy();
    }
}