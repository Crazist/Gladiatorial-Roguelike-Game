using Infrastructure.Services;
using UI.Model;

public class DeckViewModel : ViewModelBase
{
    public DeckType SelectedDeck { get; private set; }

    public void SetData(DeckType selectedDeck)
    {
        SelectedDeck = selectedDeck;
    }

    public void UpdateData(DeckType newDeck)
    {
        SelectedDeck = newDeck;
    }
}