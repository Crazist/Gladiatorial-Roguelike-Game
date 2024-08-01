using UI.Elements;
using UI.Popup;
using UI.View;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class CardPopupService
    {
        private CardPopup _cardPopup;
        private CardDragService _cardDragService;

        private bool _isDragging = false;

        [Inject]
        private void Inject(CardPopup cardPopup, CardDragService cardDragService)
        {
            _cardDragService = cardDragService;
            _cardPopup = cardPopup;
            
            SubscribeDrag();
        }

        public void SubscribeToCard(CardView cardView) => 
            SubscribeHover(cardView);

        private void SubscribeHover(CardView cardView)
        {
            cardView.OnCardHoverEnter += HandleCardHoverEnter;
            cardView.OnCardHoverExit += HandleCardHoverExit;
        }

        private void SubscribeDrag()
        {
            _cardDragService.OnCardBeginDrag += HandleCardBeginDrag;
            _cardDragService.OnCardEndDrag += HandleCardEndDrag;
        }

        private void HandleCardHoverEnter(CardView cardView)
        {
            if (!_isDragging)
                _cardPopup.Show(cardView.transform.position + new Vector3(100, 0, 0), cardView.GetCard());
        }

        private void HandleCardHoverExit(CardView cardView)
        {
            if (!_isDragging)
                _cardPopup.Hide();
        }

        private void HandleCardBeginDrag()
        {
            _isDragging = true;
            _cardPopup.Hide();
        }

        private void HandleCardEndDrag() => 
            _isDragging = false;
    }
}