using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    private void Inject(TableService tableService)
    {
        _tableService = tableService;
    }

    public override void HandleDrop(CardView cardView, CardDragService cardDragService)
    {
        if (_occupiedCard != null) return;

        _occupiedCard = cardView;
        _tableService.AddCardToEnemyTable(cardView.GetCard());
        MoveCardToDropArea(cardView);
    }

    public bool IsOccupied() => 
        _occupiedCard != null;

    public void ClearZone() => 
        _occupiedCard = null;

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

            _occupiedCard = cardView;
        });
    }
}