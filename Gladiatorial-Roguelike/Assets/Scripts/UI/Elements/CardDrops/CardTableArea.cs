using Infrastructure.Services;
using UI.Elements;
using UI.Services;
using UnityEngine;
using Zenject;

public class CardTableArea : CardDropArea
{
    private TableService _tableService;

    [Inject]
    private void Inject(TableService tableService)
    {
        _tableService = tableService;
    }

    public override void HandleDrop(CardView cardView, CardDragService cardDragService)
    {
        _tableService.AddCardToPlayerTable(cardView.GetCard());

        CenterCardInDropArea(cardView);

        cardDragService.ResetPosition(cardView);
    }

    private void CenterCardInDropArea(CardView cardView)
    {
        RectTransform cardRectTransform = cardView.GetComponent<RectTransform>();

        cardRectTransform.anchoredPosition = _rectTransform.anchoredPosition;
    }
}