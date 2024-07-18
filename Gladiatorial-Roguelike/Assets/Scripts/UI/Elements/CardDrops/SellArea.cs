using UI.Elements;
using UI.Services;
using UnityEngine;
using Zenject;

public class SellArea : CardDropArea
{
    private CardSellService _cardSellService;

    [Inject]
    private void Inject(CardSellService cardSellService)
    {
        _cardSellService = cardSellService;
    }

    public override void HandleDrop(CardView cardView, CardDragService cardDragService) => 
        _cardSellService.SellCard(cardView, () => cardDragService.ResetPosition(cardView));
}