using UI.Factory;
using UI.Type;
using Unity.VisualScripting;
using Zenject;

namespace UI.Service
{
    public class WindowService
    {
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Menu:
                    _uiFactory.CreateMenu();
                    break;
                case WindowId.ChooseDeck:
                    _uiFactory.CreateChooseDeck();
                    break;
                case WindowId.DeckWindow:
                    _uiFactory. CreateDeckWindow();
                    break;
            }
        }
    }
}