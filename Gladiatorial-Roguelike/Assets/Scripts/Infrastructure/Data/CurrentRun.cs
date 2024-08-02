using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class CurrentRun
    {
        public EnemyProgress EnemyProgress;
        public DeckProgress DeckProgress;
        public int EnemyLevel;
        public string RunId;

        public CurrentRun() => RefreshCurrentRun();

        public void RefreshCurrentRun()
        {
            RunId = Guid.NewGuid().ToString();
            EnemyProgress = new EnemyProgress();
            DeckProgress = new DeckProgress();
            EnemyLevel = 1;
        }
    }
}