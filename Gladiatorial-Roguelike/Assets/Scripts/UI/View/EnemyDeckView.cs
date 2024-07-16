using System;
using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View
{
    public class EnemyDeckView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _deckImage;

        private List<Card> _deck;

        private StaticDataService _staticDataService;
        private PlayerProgress _playerProgress;
        private DeckPopup _deckPopup;

        private DifficultyLevel _level;

        public void Init(StaticDataService staticDataService, PlayerProgress playerProgress, DeckPopup deckPopup,
            DifficultyLevel level)
        {
            _deckPopup = deckPopup;
            _playerProgress = playerProgress;
            _staticDataService = staticDataService;
            _level = level;

            SetImage();
            GetDeck();
        }

        private void SetImage() =>
             _deckImage.sprite = _staticDataService
                .ForDeck(_playerProgress.EnemyProgress.EnemyDeckType).CardBackImage;

        private void GetDeck()
        {
            switch (_level)
            {
                case DifficultyLevel.Easy:
                    _deck = _playerProgress.EnemyProgress.EasyDeck;
                    break;
                case DifficultyLevel.Intermediate:
                    _deck = _playerProgress.EnemyProgress.IntermediateDeck;
                    break;
                case DifficultyLevel.Hard:
                    _deck = _playerProgress.EnemyProgress.HardDeck;
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) =>
            _deckPopup.Show(transform.position + new Vector3(100, 0, 0), _deck);

        public void OnPointerExit(PointerEventData eventData) =>
            _deckPopup.Hide();
    }
}