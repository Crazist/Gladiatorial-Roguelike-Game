using Infrastructure.Services;
using UI.Model;

public class DeckViewModel : ViewModelBase
{
    public DeckType SelectedDeck { get; private set; }

    public DeckViewModel(DeckType selectedDeck)
    {
        SelectedDeck = selectedDeck;
    }

    public void UpdateDeck(DeckType newDeck)
    {
        SelectedDeck = newDeck;
    }
}