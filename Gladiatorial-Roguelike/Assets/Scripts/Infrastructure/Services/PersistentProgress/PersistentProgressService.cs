using Infrastructure.Data;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService
    {
        public PlayerProgress PlayerProgress;

        public PlayerProgress InitProgress() => 
            new();
    }
}