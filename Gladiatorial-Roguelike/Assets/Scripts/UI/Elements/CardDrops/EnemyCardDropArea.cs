using Infrastructure.Services;
using UI.Elements;
using UI.Services;
using UnityEngine;
using Zenject;

public class EnemyCardDropArea : CardDropArea
{
    private CardView _occupiedCard;
    private TableService _tableService;

    [Inject]
    private void Inject(TableService tableService) => 
        _tableService = tableService;

    public override void HandleDrop(CardView cardView, CardDragService cardDragService)
    {
        if (_occupiedCard != null) return;
        
        _occupiedCard = cardView;
        _tableService.AddCardToEnemyTable(cardView.GetCard());
        CenterCardInDropArea(cardView);
    }

    public bool IsOccupied() => 
        _occupiedCard != null;

    public void ClearZone() => 
        _occupiedCard = null;

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