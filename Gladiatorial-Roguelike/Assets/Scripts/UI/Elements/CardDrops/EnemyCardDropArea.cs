using Infrastructure.Services;
using Logic.Types;
using UI.Elements;
using UI.Services;
using UnityEngine;
using Zenject;

public class EnemyCardDropArea : CardDropArea
{
    private TableService _tableService;

    [Inject]
    private void Inject(TableService tableService) => 
        _tableService = tableService;

    public override void HandleDrop(CardView cardView, CardDragService cardDragService)
    {
        if (cardView.GetCard().CardData.Category == CardCategory.Special) return;

        _tableService.AddCardToEnemyTable(cardView.GetCard());
        cardView.GetCardDragHandler().ChangeDraggable(false);
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