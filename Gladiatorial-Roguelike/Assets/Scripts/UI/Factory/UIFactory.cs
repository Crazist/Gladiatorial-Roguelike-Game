using Infrastructure.Services;
using UI.Type;
using UnityEngine;
using Zenject;

namespace UI.Factory
{
    public class UIFactory
    {
        private AssetProvider _assetProvider;
        private Transform _uiRoot;
        private StaticDataService _staticDataService;

        [Inject]
       
        private void Inject(AssetProvider assetProvider, StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public void CreateMenu()
        {
            var config = _staticDataService.ForWindow(WindowId.Menu);
        }

        public void CreateUiRoot() => 
            _uiRoot = _assetProvider.InstantiateAsset<GameObject>("UI/UIRoot").transform;
    }
}