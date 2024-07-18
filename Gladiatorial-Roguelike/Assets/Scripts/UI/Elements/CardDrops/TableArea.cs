using Infrastructure.Services;
using UI.Elements;
using UnityEngine;
using Zenject;

public class TableArea : CardDropArea
{
    private TableService _tableService;

    [Inject]
    private void Inject(TableService tableService)
    {
        _tableService = tableService;
    }

    public override void HandleDrop(CardView cardView)
    {
        //_tableService.AddCardToTable(cardView.GetCardData());
        cardView.transform.localPosition = Vector3.zero;
    }
}