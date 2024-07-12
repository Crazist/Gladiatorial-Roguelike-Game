using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public DeckProgress DeckProgress;
        public Profile Profile;

        public PlayerProgress()
        {
            DeckProgress = new DeckProgress();
            Profile = new Profile();
        }
    }
}