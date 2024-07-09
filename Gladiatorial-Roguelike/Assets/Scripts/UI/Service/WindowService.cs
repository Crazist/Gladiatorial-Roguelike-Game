using Infrastructure;
using UI.Type;
using Zenject;

namespace UI.Service
{
    public class WindowService
    {
        private const string deckSelect = "DeckSelect";
        
        private SceneLoader _sceneLoader;

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.ChooseDeck:
                    _sceneLoader.Load(deckSelect);
                    break;
            }
        }
    }
}