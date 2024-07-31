using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Logic.Types;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.Elements.CardDrops
{
    public class CardTableArea : CardDropArea
    {
        private TableService _tableService;

        [Inject]
        private void Inject(TableService tableService) => 
            _tableService = tableService;

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            if (cardView.GetCard().CardData.Category == CardCategory.Special)
            {
                cardDragService.ResetPosition(cardView);
                return;
            }
             
            _tableService.GetPlayerTableViews().Add(cardView);
            _tableService.GetPlayerHandViews().Remove(cardView);
            
            OccupiedCard = cardView;
        
            cardView.GetCardDragHandler().ChangeDraggable(false);
            cardView.State = CardState.OnTable;
            
            CenterCardInDropArea(cardView);
        }

        private void CenterCardInDropArea(CardView cardView)
        {
            RectTransform cardRectTransform = cardView.GetRectTransform();

            cardRectTransform.SetParent(_rectTransform, false);

            cardRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cardRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            cardRectTransform.anchoredPosition = Vector2.zero;

            cardRectTransform.sizeDelta = new Vector2(50, 70);
        }
    }
}