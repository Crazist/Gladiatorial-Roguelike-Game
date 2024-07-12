using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using Logic.Enteties;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PermaDeckWindow : WindowBase
    {
        [SerializeField] private Transform _cardsParent; 
        [SerializeField] private CardView _cardPrefab;
        
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        protected override void OnAwake() => 
            CreateCards();

        private void CreateCards()
        {
            List<Card> cards = _persistentProgressService.PlayerProgress.Profile.PermaDeck.Cards;
            
            foreach (Card card in cards)
            {
                CardView cardView = Instantiate(_cardPrefab, _cardsParent);
             //   cardView.Initialize(card);
            }
        }
    }
}