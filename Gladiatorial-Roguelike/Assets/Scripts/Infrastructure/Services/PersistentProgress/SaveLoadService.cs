using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure
{
    public class SaveLoadService
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}