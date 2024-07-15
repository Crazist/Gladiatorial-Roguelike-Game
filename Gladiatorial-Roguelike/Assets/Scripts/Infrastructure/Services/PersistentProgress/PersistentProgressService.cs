using Infrastructure.Data;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService
    {
        public PlayerProgress PlayerProgress;

        public void ClearProgress()
        {
            PlayerProgress = new PlayerProgress();
        }
    }
}