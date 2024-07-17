using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SaveLoadService
    {
        private const string ProgressKey = "Progress";
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService) =>
            _persistentProgressService = persistentProgressService;

        public void SaveProgress()
        {
            string json = JsonConvert.SerializeObject(_persistentProgressService.PlayerProgress, Formatting.Indented);
            PlayerPrefs.SetString(ProgressKey, json);
        }

        public PlayerProgress LoadProgress()
        {
            string json = PlayerPrefs.GetString(ProgressKey);
            return string.IsNullOrEmpty(json) 
                ? null 
                : JsonConvert.DeserializeObject<PlayerProgress>(json);
        }
    }
}