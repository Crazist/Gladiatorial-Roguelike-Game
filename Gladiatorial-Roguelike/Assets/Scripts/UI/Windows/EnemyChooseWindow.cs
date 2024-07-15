using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EnemyChooseWindow : WindowBase
    {
        [SerializeField] private Button _playersDeck;
        [SerializeField] private Image _playersDeckImage;
        
        private PersistentProgressService _persistentProgressService;
        private StaticDataService _staticDataService;
        private DeckViewModel _deckViewModel;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, StaticDataService staticDataService,
            DeckViewModel deckViewModel)
        {
            _deckViewModel = deckViewModel; 
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;
            
            SetPlayerDeckImage();
            RegisterBtn();
        }

        private void RegisterBtn() => 
            _playersDeck.onClick.AddListener(OpenDeckWindow);

        private void OpenDeckWindow() =>
            _deckViewModel.SetContinueBtn(false);
        private void SetPlayerDeckImage() => 
            _playersDeckImage.sprite = LoadImage();

        private Sprite LoadImage() => 
            _staticDataService.ForDeck(_persistentProgressService.PlayerProgress.DeckProgress.CurrentDeck).CardBackImage;
    }
}