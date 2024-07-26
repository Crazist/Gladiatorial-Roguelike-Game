using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class DeckBattleView : MonoBehaviour
    {
        [SerializeField] private Image _deckImage;
        [SerializeField] private TMP_Text _deckCountText;

        public void SetDeckImage(Sprite sprite) => _deckImage.sprite = sprite;

        public void UpdateDeckCount(int currentCount, int maxCount) =>
            _deckCountText.text = $"{currentCount}/{maxCount}";
    }
}