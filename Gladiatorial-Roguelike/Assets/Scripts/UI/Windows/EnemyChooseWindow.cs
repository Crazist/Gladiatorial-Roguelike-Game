using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Logic.Types;
using TMPro;
using UI.Elements;
using UI.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EnemyChooseWindow : WindowBase
    {
        [SerializeField] private List<EnemyDeckView> _enemyDecks;

        [SerializeField] private Button _playersDeck;
        [SerializeField] private Image _playersDeckImage;
        [SerializeField] private TMP_Text _enemyName;

        private PlayerProgress _playerProgress;
        private StaticDataService _staticDataService;
        private DeckViewModel _deckViewModel;
        private DeckPopup _deckPopup;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, StaticDataService staticDataService,
            DeckViewModel deckViewModel, DeckPopup deckPopup)
        {
            _deckPopup = deckPopup;
            _deckViewModel = deckViewModel;
            _staticDataService = staticDataService;
            _playerProgress = persistentProgressService.PlayerProgress;

            SetPlayerDeckImage();
            RegisterBtn();
            SetEnemyname();
            InitEnemyDecks();
        }

        private void SetEnemyname() =>
            _enemyName.text = "Level " + _playerProgress.CurrentRun.Level + ": " +
                              _playerProgress.EnemyProgress.EnemyDeckType;

        private void RegisterBtn() =>
            _playersDeck.onClick.AddListener(OpenDeckWindow);

        private void OpenDeckWindow() =>
            _deckViewModel.SetContinueBtn(false);

        private void SetPlayerDeckImage() =>
            _playersDeckImage.sprite = LoadImage();

        private Sprite LoadImage() =>
            _staticDataService.ForDeck(_playerProgress.DeckProgress.CurrentDeck).CardBackImage;

        private void InitEnemyDecks()
        {
            _enemyDecks[0].Init(_staticDataService, _playerProgress, _deckPopup, DifficultyLevel.Easy);
            _enemyDecks[1].Init(_staticDataService, _playerProgress, _deckPopup, DifficultyLevel.Intermediate);
            _enemyDecks[2].Init(_staticDataService, _playerProgress, _deckPopup, DifficultyLevel.Hard);
        }
    }
}