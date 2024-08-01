using System;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using Logic.Types;
using Zenject;

namespace Infrastructure.Services.Currency
{
    public class RewardService
    {
        private readonly StaticDataService _staticDataService;
        private readonly Random _random;
        private readonly PersistentProgressService _persistentProgressService;
        private readonly BattleResultService _battleResultService;

        [Inject]
        public RewardService(StaticDataService staticDataService,
            PersistentProgressService persistentProgressService, BattleResultService battleResultService)
        {
            _battleResultService = battleResultService;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _random = new Random();
        }

        public void GrantReward()
        {
            DeckComplexity chosenDeck = _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.ChoosenDeck;
            RewardRange rewardRange = _staticDataService.ForRewardRangeForComplexity(chosenDeck);

            int reward = _random.Next(rewardRange.Min, rewardRange.Max + 1);

            _battleResultService.CurrencyReward = reward;

            _persistentProgressService.PlayerProgress.Profile.Currency += reward;
        }
    }
}