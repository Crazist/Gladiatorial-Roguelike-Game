using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class CurrentRun
    {
        public EnemyProgress EnemyProgress;
        public DeckProgress DeckProgress;

        public int EnemyLevel;
        public CurrentRun() => RefreshCurrentRun();

        public void RefreshCurrentRun()
        {
            EnemyProgress = new EnemyProgress();
            DeckProgress = new DeckProgress();
            EnemyLevel = 1;
        }
    }
}