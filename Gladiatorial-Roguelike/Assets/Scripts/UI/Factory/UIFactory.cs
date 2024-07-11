using System.Collections.Generic;
using Infrastructure.Services;
using UI.Model;
using UI.Type;
using UnityEngine;
using Zenject;

namespace UI.Factory
{
    public class UIFactory
    {
        private const string UIRoot = "UI/UIRoot";
        
        private Dictionary<WindowId, ViewModelBase> _viewModels = new();
        
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
        public void CreateDeckWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.DeckWindow);
            Object.Instantiate(config.Prefab, _uiRoot);
            
            _viewModels.Add(WindowId.DeckWindow, new DeckViewModel(DeckType.None));
        }
        public T GetViewModel<T>(WindowId id) where T : ViewModelBase
        {
            if (_viewModels.TryGetValue(id, out var viewModel))
            {
                return viewModel as T;
            }

            return null;
        }
    }
}