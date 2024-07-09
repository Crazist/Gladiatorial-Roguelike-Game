using System.Collections.Generic;
using System.Linq;
using Data;
using Logic.Cards;
using UI.Type;
using Zenject;

namespace Infrastructure.Services
{
    public class StaticDataService
    {
        private const string StaticDataDeckPath = "Data/Decks";
        private const string StaticDataWindowsPath = "Data/Window/WindowStaticData";

        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<DeckType, DeckData> _decks;

        private AssetProvider _assetProvider;

        [Inject]
        private void Inject(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;

            LoadResources();
        }

        private void LoadResources()
        {
            LoadDecks();
            LoadWindowConfig();
        }

        public DeckData ForDeck(DeckType deckType) =>
            _decks.GetValueOrDefault(deckType);

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.GetValueOrDefault(windowId);

        private void LoadDecks() =>
            _decks = _assetProvider.LoadAllAssets<DeckData>(StaticDataDeckPath)
                .ToDictionary(deckData => deckData.DeckType, x => x);

        private void LoadWindowConfig() =>
            _windowConfigs = _assetProvider.LoadAsset<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
    }
}