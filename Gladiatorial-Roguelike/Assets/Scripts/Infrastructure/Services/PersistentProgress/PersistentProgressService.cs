using Infrastructure.Data;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService
    {
        public PlayerProgress PlayerProgress;

        public PlayerProgress InitProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress();

            playerProgress.CurrentRun.Level = 1;
           
            return playerProgress;
        }

        public void ClearProgress() => 
            PlayerProgress = InitProgress();
    }
}