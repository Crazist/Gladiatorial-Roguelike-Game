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
        private const string StaticDataDeckPath = "Data/Decks/RomanDeck";
        private const string StaticDataWindowsPath = "Data/Window/WindowStaticData";

        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        
        private DeckData _deck;
        private AssetProvider _assetProvider;

        [Inject]
        private void Inject(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;

            LoadResources();
        }

        private void LoadResources()
        {
            LoadRomanDeck();
            LoadWindowConfig();
        }

        public DeckData ToRomanDeck() =>
            _deck;

        private void LoadRomanDeck() =>
            _deck = _assetProvider.LoadAsset<DeckData>(StaticDataDeckPath);

        private void LoadWindowConfig() =>
            _windowConfigs = _assetProvider.LoadAsset<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.GetValueOrDefault(windowId);
    }
}