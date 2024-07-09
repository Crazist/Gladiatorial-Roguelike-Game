using UI.Factory;
using UI.Type;
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
                case WindowId.ChooseDeck:
                    _uiFactory.CreateChooseDeck();
                    break;
            }
        }
    }
}