using Infrastructure.Services;
using UI.Elements;
using UI.Type;
using UnityEngine;
using Zenject;

namespace UI.Factory
{
    public class UIFactory
    {
        private const string UIRoot = "UI/UIRoot";
        private const string DebugPanelPath = "UI/DebugPanel";
        private const string TurnIndicator = "UI/TurnIndicator";
        private const string AttackArrowPath = "UI/AttackArrow";
        public  Canvas UI { get; private set; }
        public Canvas WorldSpaceCanvas { get; private set; }
        public  TurnIndicator TurnShower { get; private set; }

        private AssetProvider _assetProvider;
        private Transform _uiRoot;
        private StaticDataService _staticDataService;


        [Inject]
       
        private void Inject(AssetProvider assetProvider, StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public void CreateUiRoot()
        { 
          UI = _assetProvider.InstantiateAsset<Canvas>(UIRoot);
          _uiRoot = UI.gameObject.transform;
        }

        public void CreateTurnIndicator() => 
            TurnShower = _assetProvider.InstantiateAsset<TurnIndicator>(TurnIndicator, _uiRoot);

        public void CreateDebugPanel() => 
            Object.Instantiate(_assetProvider.LoadAsset<GameObject>(DebugPanelPath), _uiRoot);

        public AttackArrow CreateAttackArrow() => 
            _assetProvider.InstantiateAsset<AttackArrow>(AttackArrowPath, _uiRoot);

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
        }

        public void CreatePermaDeckWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.PermaDeck);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateEnemyChooseWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.EnemyChoose);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateTableWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.TableWindow);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
        public void CreateVictoryWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.VictoryWindow);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
        public void CreateBlackSmithWindow()
        {
            var config = _staticDataService.ForWindow(WindowId.BlackSmithWindow);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
    }
}