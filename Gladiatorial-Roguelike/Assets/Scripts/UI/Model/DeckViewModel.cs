using Infrastructure.Services;
using UI.Model;

public class DeckViewModel : ViewModelBase
{
    public DeckType SelectedDeck { get; private set; }
    public bool HasContinueBtn { get; private set; }

    public void SetType(DeckType selectedDeck) => 
        SelectedDeck = selectedDeck;

    public void SetContinueBtn(bool hasContinueBtn) => 
        HasContinueBtn = hasContinueBtn;
}