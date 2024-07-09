using Infrastructure.Services;
using UI.Service;
using UI.Type;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.View
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Image _deckImage;

        [SerializeField] private Button _openDeckBtn;
        
        [SerializeField] private DeckType _type;

        private StaticDataService _staticData;
        private WindowService _windowService;

        [Inject]
        public void Inject(StaticDataService staticDataService, WindowService windowService)
        {
            _windowService = windowService;
            _staticData = staticDataService;
        }

        private void Start()
        {
            _openDeckBtn.onClick.AddListener(() =>_windowService.Open(WindowId.Menu));
            
            VisualizeDecks();
        }

        private void VisualizeDecks() =>
            _deckImage.sprite = _staticData.ForDeck(_type)?.CardBackImage
                ? _staticData.ForDeck(_type).CardBackImage
                : _deckImage.sprite;
    }
}