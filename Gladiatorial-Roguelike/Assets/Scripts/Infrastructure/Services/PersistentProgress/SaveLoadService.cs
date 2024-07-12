using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
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
            _persistentProgressService.PlayerProgress.Profile.Level = 10;
            string test = _persistentProgressService.PlayerProgress.ToJson();
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.PlayerProgress.ToJson());
            _persistentProgressService.PlayerProgress.Profile.Level = 0;
        }

        public PlayerProgress LoadProgress() =>
            _persistentProgressService.PlayerProgress =
                PlayerPrefs.GetString(ProgressKey)?
                    .ToDeserialized<PlayerProgress>();
    }
}