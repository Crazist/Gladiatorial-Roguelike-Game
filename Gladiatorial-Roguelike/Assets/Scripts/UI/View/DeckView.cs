using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI.Factory;
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
       
        [SerializeField] private GameObject _lockerImage;

        [SerializeField] private Button _openDeckBtn;
        
        [SerializeField] private int _levelNeededToOpen;
        
        [SerializeField] private DeckType _type;

        private StaticDataService _staticData;
        private WindowService _windowService;
        private PersistentProgressService _persistentProgressService;
        private DeckViewModel _deckViewModel;

        [Inject]
        public void Inject(StaticDataService staticDataService, WindowService windowService,
            PersistentProgressService persistentProgressService, DeckViewModel deckViewModel)
        {
            _deckViewModel = deckViewModel;
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
            _staticData = staticDataService;
        }

        private void Start()
        {
            _openDeckBtn.onClick.AddListener(OnDeckClick);
            
            VisualizeDecks();
        }

        private void OnDeckClick()
        {
            UpdateDeckWindowModel();
            _windowService.Open(WindowId.DeckWindow);
        }

        private void UpdateDeckWindowModel()
        {
            _deckViewModel.SetType(_type);
            _deckViewModel.SetContinueBtn(true);
        }

        private void VisualizeDecks()
        {
            _deckImage.sprite = _staticData.ForDeck(_type).CardBackImage;
            _lockerImage.SetActive(_persistentProgressService.PlayerProgress.Profile.Level < _levelNeededToOpen);
        }
    }
}