using Infrastructure.Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class EnemyDeckView : MonoBehaviour
    {
        [SerializeField] private Image _deckImage;
        
        private StaticDataService _staticDataService;
        private PlayerProgress _playerProgress;

        public void Init(StaticDataService staticDataService, PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _staticDataService = staticDataService;

            SetImage();
        }

        private void SetImage() =>
            _deckImage.sprite = _staticDataService
                .ForDeck(_playerProgress.EnemyProgress.EnemyDeckType).CardBackImage;
    }
}