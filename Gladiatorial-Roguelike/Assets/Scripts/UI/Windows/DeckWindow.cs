using Infrastructure.Services;
using Zenject;

namespace UI
{
    public class DeckWindow : WindowBase
    {
        private DeckType DeckType;

        [Inject]
        private void Inject(DeckViewModel deckViewModel) => 
            DeckType = deckViewModel.SelectedDeck;
    }
}