using System.Collections.Generic;
using System.Linq;
using Data.Cards;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Enteties;
using Logic.Types;
using Zenject;

namespace Infrastructure.Services
{
    public class EnemyService
    {
        private const string DataFractionsPath = "Data/Fraction";

        private PersistentProgressService _persistentProgress;
        private AssetProvider _assetProvider;
        private FractionDeckData _fractionConfig;
        private Factory _factory;
        private SaveLoadService _saveLoadService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgress, AssetProvider assetProvider, Factory factory,
            SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _factory = factory;
            _assetProvider = assetProvider;
            _persistentProgress = persistentProgress;
        }

        public void InitEnemyDecks()
        {
            _fractionConfig = null;

            LoadAndSelectRandomConfig();

            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.EasyDeck.Cards = CreateDeck(5, 3);
            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.IntermediateDeck.Cards = CreateDeck(6, 4);
            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.HardDeck.Cards = CreateDeck(8, 5);
        }

        public bool NeedRefreshEnemy() =>
            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.HardDeck.IsSkipped == EnemyDeckState.Defeated;

        public void RefreshEnemy()
        {
            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.RefreshEnemy();
            _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.EnemyDeckType = DeckType.None;
            InitEnemyDecks();
            _saveLoadService.SaveProgress();
        }

        private void LoadAndSelectRandomConfig()
        {
            FractionDeckData[] configs = _assetProvider.LoadAllAssets<FractionDeckData>(DataFractionsPath);

            if (LoadEnemyConfigIfExist(configs)) return;

            EnemyProgress enemyProgress = _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress;
           
            if (enemyProgress.UsedDecks.Count == configs.Length)
            {
                enemyProgress.UsedDecks.Clear();
            }

            FractionDeckData[] availableConfigs = configs.Where(config => !enemyProgress.UsedDecks.Contains(config.DeckType)).ToArray();
            _fractionConfig = availableConfigs[UnityEngine.Random.Range(0, availableConfigs.Length)];
            enemyProgress.EnemyDeckType = _fractionConfig.DeckType;
            enemyProgress.UsedDecks.Add(_fractionConfig.DeckType);
        }

        private bool LoadEnemyConfigIfExist(FractionDeckData[] configs)
        {
            if (_persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.EnemyDeckType != DeckType.None)
            {
                _fractionConfig = configs
                    .FirstOrDefault(config =>
                        config.DeckType == _persistentProgress.PlayerProgress.CurrentRun.EnemyProgress.EnemyDeckType);
            }

            return _fractionConfig != null;
        }

        private List<Card> CreateDeck(int unitCount, int specialCount)
        {
            IEnumerable<CardData> unitCards =
                _fractionConfig.Cards.Where(card => card.Category == CardCategory.Unit).Take(unitCount);
            IEnumerable<CardData> specialCards = _fractionConfig.Cards
                .Where(card => card.Category == CardCategory.Special).Take(specialCount);

            return _factory.CreateCards(unitCards.Concat(specialCards));
        }
    }
}
