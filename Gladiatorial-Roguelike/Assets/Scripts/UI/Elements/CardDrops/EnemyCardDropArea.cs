using DG.Tweening;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.CardsServices;
using Logic.Types;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.Elements.CardDrops
{
    public class EnemyCardDropArea : CardDropArea
    {
        private TableService _tableService;

        [Inject]
        private void Inject(TableService tableService) => 
            _tableService = tableService;

        public override void HandleDrop(CardView cardView, CardDragService cardDragService)
        {
            if (OccupiedCard != null) return;

            OccupiedCard = cardView;
            cardView.State = CardState.OnTable;
            _tableService.GetEnemyTableViews().Add(cardView);
            _tableService.GetEnemyHandViews().Remove(cardView);
            
            MoveCardToDropArea(cardView);
        }

        public void ClearZone() => 
            OccupiedCard = null;

        private void MoveCardToDropArea(CardView cardView)
        {
            RectTransform cardRectTransform = cardView.GetRectTransform();
        
            Vector3 worldPosition = cardRectTransform.position;

            cardRectTransform.SetParent(_rectTransform.root, false);
            cardRectTransform.position = worldPosition;

            cardRectTransform.DOMove(_rectTransform.position, 0.5f).OnComplete(() =>
            {
                cardRectTransform.SetParent(_rectTransform, false);
                cardRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                cardRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                cardRectTransform.anchoredPosition = Vector2.zero;
                cardRectTransform.sizeDelta = new Vector2(50, 70);

                OccupiedCard = cardView;
            });
        }
    }
}