using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Logic.Types;
using Zenject;

namespace Infrastructure.Services.Leaderboard
{
    public class LeaderboardService
    {
        private PersistentProgressService _persistentProgressService;
        private StaticDataService _staticDataService;

        [Inject]
        public void Inject(PersistentProgressService persistentProgressService, StaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        public void AddScore()
        {
            int points = CalculatePoints();
            Profile profile = _persistentProgressService.PlayerProgress.Profile;
            string runId = _persistentProgressService.PlayerProgress.CurrentRun.RunId;

            LeaderboardEntry newEntry = new LeaderboardEntry("Player", points, runId);
            AddOrUpdateLeaderboardEntry(profile.Leaderboard, newEntry);
        }

        public List<LeaderboardEntry> GetTopEntries(int count)
        {
            List<LeaderboardEntry> leaderboard = _persistentProgressService.PlayerProgress.Profile.Leaderboard;
            return leaderboard.GetRange(0, Math.Min(count, leaderboard.Count));
        }

        private int CalculatePoints()
        {
            ScoreConfig scoreSettings = _staticDataService.ForScoreConfig();
            DeckComplexity complexity = _persistentProgressService.PlayerProgress.CurrentRun.EnemyProgress.ChoosenDeck;

            return scoreSettings.GetScoreForComplexity(complexity);
        }

        private void AddOrUpdateLeaderboardEntry(List<LeaderboardEntry> leaderboard, LeaderboardEntry newEntry)
        {
            var existingEntry = leaderboard.FirstOrDefault(entry => entry.RunId == newEntry.RunId);

            if (existingEntry != null)
            {
                existingEntry.Score += newEntry.Score; 
            }
            else
            {
                leaderboard.Add(newEntry);
            }

            leaderboard.Sort((x, y) => y.Score.CompareTo(x.Score));

            if (leaderboard.Count > 10)
            {
                leaderboard.RemoveAt(leaderboard.Count - 1);
            }
        }
    }
}
