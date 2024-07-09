using Infrastructure.Services;
using UI.Type;
using UnityEngine;
using Zenject;

namespace UI.Factory
{
    public class UIFactory
    {
        private const string UIRoot = "UI/UIRoot";
        
        private AssetProvider _assetProvider;
        private Transform _uiRoot;
        private StaticDataService _staticDataService;

        [Inject]
       
        private void Inject(AssetProvider assetProvider, StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public void CreateUiRoot() => 
            _uiRoot = _assetProvider.InstantiateAsset<GameObject>(UIRoot).transform;

        public void CreateMenu()
        {
            var config = _staticDataService.ForWindow(WindowId.Menu);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateChooseDeck()
        {
            var config = _staticDataService.ForWindow(WindowId.ChooseDeck);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
    }
}