using Infrastructure.Data;

namespace Infrastructure.Interface
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}