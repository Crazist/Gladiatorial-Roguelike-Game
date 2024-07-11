using Infrastructure.Services;
using UI.Factory;
using UI.Type;
using Zenject;

namespace UI
{
    public class DeckWindow : WindowBase
    {
        private DeckType DeckType;

        [Inject]
        private void Inject(UIFactory uiFactory) => 
            DeckType = uiFactory.GetViewModel<DeckViewModel>(WindowId.DeckWindow).SelectedDeck;
    }
}