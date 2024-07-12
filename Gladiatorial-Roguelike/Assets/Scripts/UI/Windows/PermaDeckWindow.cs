using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.PersistentProgress;
using Logic.Cards;
using Logic.Enteties;
using UI.Elements;
using UI.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{
    public class PermaDeckWindow : WindowBase
    {
        [SerializeField] private Transform _cardsParent; 
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private RectTransform _sellArea;
        
        private PersistentProgressService _persistentProgressService;

        private bool _isDragging = false;
        private UIFactory _uiFactory;
        private CardPopup _cardPopup;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, UIFactory uiFactory, 
            CardPopup cardPopup)
        {
            _cardPopup = cardPopup;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
        }

        private void Start() => 
            CreateCards();

        private void CreateCards()
        {
            List<Card> cards = _persistentProgressService.PlayerProgress.Profile.PermaDeck.Cards;
            
            foreach (Card card in cards)
            {
                CardView cardView = Instantiate(_cardPrefab, _cardsParent);
                
                cardView.Initialize(card.CardData);

                var cardDragHandler = cardView.GetCardDragHandler();
                
                cardDragHandler.Init(cardView, _uiFactory.UI);

                cardView.OnCardHoverEnter += HandleCardHoverEnter;
                cardView.OnCardHoverExit += HandleCardHoverExit;

                cardDragHandler.OnCardBeginDrag += HandleCardBeginDrag;
                cardDragHandler.OnCardEndDrag += HandleCardEndDrag;
            }
        }

        private void HandleCardHoverEnter(CardView cardView)
        {
            if (!_isDragging)
                _cardPopup.Show(cardView.transform.position + new Vector3(100, 0, 0), cardView.GetCardData());
        }

        private void HandleCardHoverExit(CardView cardView)
        {
            if (!_isDragging)
                _cardPopup.Hide();
        }

        private void HandleCardBeginDrag(CardView cardView, PointerEventData eventData)
        {
            _isDragging = true;
            _cardPopup.Hide();
        }

        private void HandleCardEndDrag(CardView cardView, PointerEventData eventData)
        {
            _isDragging = false;

            if (RectTransformUtility.RectangleContainsScreenPoint(_sellArea, eventData.position, eventData.pressEventCamera))
            {
                SellCard(cardView);
            }
            else
            {
                cardView.GetCardDragHandler().ResetPosition();
            }
        }

        private void SellCard(CardView cardView)
        {
            CardData cardData = cardView.GetCardData();
            string cardName = cardData.CardName;

            List<Card> cards = _persistentProgressService.PlayerProgress.Profile.PermaDeck.Cards;
            
            Card cardToRemove = cards.FirstOrDefault(card => card.CardName == cardName);

            cards.Remove(cardToRemove);
            
            Destroy(cardView.gameObject);
        }
    }
}
