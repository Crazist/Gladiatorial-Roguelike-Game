using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.StateMachines;
using Infrastructure.States;
using Logic.Entities;
using Services;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class EnemyDeckView : MonoBehaviour
    {
        [SerializeField] private DeckHoverHandler _deckHoverHandler;
        [SerializeField] private Button _skipBtn;
        [SerializeField] private Button _playBtn;
        [SerializeField] private GameObject _holderBtnsAndText;
        [SerializeField] private Image _deckImage;
        [SerializeField] private TMP_Text _stateText;

        private Dictionary<Card, int> _sortedCardsWithCount;
        
        private StaticDataService _staticDataService;
        private DeckPopup _deckPopup;
        private EnemyDeck _enemyDeck;
        private SaveLoadService _saveLoadService;
        private GameStateMachine _gameStateMachine;
        
        private DeckType _enemyDeckType;
        private EnemyProgress _enemyProgress;

        public void Init(StaticDataService staticDataService, EnemyDeck enemyDeck, DeckPopup deckPopup, SaveLoadService saveLoadService,
            GameStateMachine gameStateMachine, CardSortingService cardSortingService, EnemyProgress enemyProgress)
        {
            _enemyProgress = enemyProgress;
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _enemyDeckType = enemyProgress.EnemyDeckType;
            _enemyDeck = enemyDeck;
            _deckPopup = deckPopup;
            _staticDataService = staticDataService;

            _sortedCardsWithCount = cardSortingService.GroupAndSortCards(enemyDeck.Cards);

            RegisterBtns();
            SetImage();
            DisableBtnsAndText();
            RegisterHoverHandler();
            SetStateText();
        }

        private void RegisterBtns()
        {
            _skipBtn.onClick.AddListener(UpdateBtnAndSave);
            _playBtn.onClick.AddListener(StartGame);
        }

        private void RegisterHoverHandler()
        {
            _deckHoverHandler.OnHoverEnter += ShowPopup;
            _deckHoverHandler.OnHoverExit += HidePopup;
        }

        private void StartGame()
        {
            _enemyProgress.ChoosenDeck = _enemyDeck.DeckComplexity;
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void UpdateBtnAndSave()
        {
            _enemyDeck.IsSkipped = EnemyDeckState.Skipped;
            _saveLoadService.SaveProgress();
            
            DisableBtnsAndText();
            SetStateText();
        }

        private void SetStateText() => 
            _stateText.text = _enemyDeck.IsSkipped.ToString();

        private void SetImage() =>
             _deckImage.sprite = _staticDataService
                .ForDeck(_enemyDeckType).CardBackImage;

        private void DisableBtnsAndText()
        {
            _stateText.gameObject.SetActive(_enemyDeck.IsSkipped != EnemyDeckState.None);
            _holderBtnsAndText.gameObject.SetActive(_enemyDeck.DeckComplexity != DeckComplexity.Hard && 
                                                    _enemyDeck.IsSkipped == EnemyDeckState.None);
            _playBtn.gameObject.SetActive(_enemyDeck.IsSkipped == EnemyDeckState.None);
        }

        private void ShowPopup(Vector3 position)
        {
            if(_enemyDeck.IsSkipped == EnemyDeckState.None)
            _deckPopup.Show(position + new Vector3(100, 0, 0), _sortedCardsWithCount);
        }

        private void HidePopup(Vector3 position) =>
            _deckPopup.Hide();
    }
}
