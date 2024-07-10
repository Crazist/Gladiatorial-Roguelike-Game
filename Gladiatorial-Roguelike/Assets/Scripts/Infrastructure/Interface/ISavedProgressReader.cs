using Infrastructure.Data;

namespace Infrastructure.Interface
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}