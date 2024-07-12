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

        public void SaveProgress() => 
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.PlayerProgress.ToJson());

        public PlayerProgress LoadProgress() =>
            _persistentProgressService.PlayerProgress =
                PlayerPrefs.GetString(ProgressKey)?
                    .ToDeserialized<PlayerProgress>();
    }
}