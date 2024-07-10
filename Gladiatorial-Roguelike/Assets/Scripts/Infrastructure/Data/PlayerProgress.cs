using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public DeckProgress DeckProgress;
        public CurrentRun CurrentRun;

        public PlayerProgress()
        {
            DeckProgress = new DeckProgress();
            CurrentRun = new CurrentRun();
        }
    }
}