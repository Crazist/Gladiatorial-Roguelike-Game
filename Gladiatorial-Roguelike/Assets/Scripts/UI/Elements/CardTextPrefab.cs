using Logic.Types;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class CardTextPrefab : MonoBehaviour
    {
        [SerializeField] private CardRarityColor _colorConfig;
       
        [SerializeField] private TMP_Text _cardText;
        [SerializeField] private TMP_Text _cardRarity;
        public void SetCardData(CardRarity cardRarity, string cardRareAndName)
        {
            _cardRarity.text = cardRarity.ToString();
            _cardText.text = cardRareAndName;

            SetColor(cardRarity);
        }

        private void SetColor(CardRarity cardRarity)
        {
            if (_colorConfig.RarityColorDictionary.TryGetValue(cardRarity, out var color))
                _cardRarity.color = color;
        }
    }
}
