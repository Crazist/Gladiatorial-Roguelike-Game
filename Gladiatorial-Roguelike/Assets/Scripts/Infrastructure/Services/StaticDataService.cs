using System.Collections.Generic;
using System.Linq;
using Data;
using Logic.Cards;
using Logic.Enteties;
using Logic.Types;
using UI.Type;
using Zenject;

namespace Infrastructure.Services
{
    public class StaticDataService
    {
        private const string StaticDataDeckPath = "Data/Decks";
        private const string StaticDataWindowsPath = "Data/Window/WindowStaticData";
        private const string StaticDataRewardPath = "Data/Rewards";

        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<DeckType, DeckData> _decks;
        private Dictionary<DeckComplexity, RewardRange> _rewardRanges;

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
            LoadRewardRanges();
        }

        public DeckData ForDeck(DeckType deckType) =>
            _decks.GetValueOrDefault(deckType);

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.GetValueOrDefault(windowId);

        public RewardRange GetRewardRangeForComplexity(DeckComplexity complexity) =>
            _rewardRanges.GetValueOrDefault(complexity);

        private void LoadDecks() =>
            _decks = _assetProvider.LoadAllAssets<DeckData>(StaticDataDeckPath)
                .ToDictionary(deckData => deckData.DeckType, x => x);

        private void LoadWindowConfig() =>
            _windowConfigs = _assetProvider.LoadAsset<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

        private void LoadRewardRanges() => _rewardRanges = _assetProvider.LoadAsset<Rewards>(StaticDataRewardPath)
            .Ranges.ToDictionary(rewardRange => rewardRange.Complexity, x => x);
    }
}