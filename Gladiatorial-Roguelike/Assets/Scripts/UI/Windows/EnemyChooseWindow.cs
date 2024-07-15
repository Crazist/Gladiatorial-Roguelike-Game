using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EnemyChooseWindow : WindowBase
    {
        [SerializeField] private Image _playersDeckImage;

        private PersistentProgressService _persistentProgressService;
        private StaticDataService _staticDataService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;

            SetPlayerDeckImage();
        }

        private void SetPlayerDeckImage() =>
            _playersDeckImage.sprite = LoadImage();

        private Sprite LoadImage() =>
            _staticDataService.ForDeck(_persistentProgressService.PlayerProgress.DeckProgress.CurrentDeck)
                .CardBackImage;
    }
}