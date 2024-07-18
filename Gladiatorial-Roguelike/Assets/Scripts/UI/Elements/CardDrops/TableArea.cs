using Infrastructure.Services;
using UI.Elements;
using UI.Services;
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
        // Add the card to the table and reset its position appropriately.
       // _tableService.PlaceCardOnTable(cardView);
        cardDragService.ResetPosition(cardView);
    }
}