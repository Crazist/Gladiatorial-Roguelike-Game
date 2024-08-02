using System.Collections.Generic;
using Data;
using Infrastructure.Services.Leaderboard;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Windows
{
    public class LeaderboardWindow : WindowBase
    {
        [SerializeField] private RectTransform _leaderboardContainer;
        [SerializeField] private TMP_Text _leaderboardEntryPrefab;

        private LeaderboardService _leaderboardService;

        [Inject]
        public void Inject(LeaderboardService leaderboardService) => 
            _leaderboardService = leaderboardService;

        private void Start()
        {
            var leaderboard = _leaderboardService.GetTopEntries(10);
            DisplayLeaderboard(leaderboard);
        }

        public void DisplayLeaderboard(List<LeaderboardEntry> leaderboard)
        {
            foreach (var entry in leaderboard)
            {
                var entryText = Instantiate(_leaderboardEntryPrefab, _leaderboardContainer);
                entryText.text = $"{entry.PlayerName}: {entry.Score} points";
            }
        }
    }
}