using System;

namespace Data
{
    [Serializable]
    public class LeaderboardEntry
    {
        public string PlayerName;
        public int Score;
        public string RunId;

        public LeaderboardEntry(string playerName, int score, string runId)
        {
            PlayerName = playerName;
            Score = score;
            RunId = runId;
        }
    }
}