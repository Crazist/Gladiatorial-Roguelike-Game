using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class CardTextPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardText;
        
        public void SetCardData(string cardRareAndName) => 
            _cardText.text = cardRareAndName;
    }
}
