using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public DeckProgress DeckProgress;
        public Profile Profile;
        public EnemyProgress EnemyProgress;
        public PlayerProgress()
        {
            EnemyProgress = new EnemyProgress();
            DeckProgress = new DeckProgress();
            Profile = new Profile();
        }
    }
}