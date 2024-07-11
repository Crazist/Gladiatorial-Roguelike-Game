using Logic.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class CardPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardName;
        [SerializeField] private TMP_Text _cardAttack;
        [SerializeField] private TMP_Text _cardDefense;
        [SerializeField] private TMP_Text _cardXp;
        [SerializeField] private TMP_Text _cardType;
        [SerializeField] private GameObject _popUp;
        public void Show(Vector3 position, CardData cardData)
        {
            UpdateData(cardData);
            SetPosition(position);
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void UpdateData(CardData cardData)
        {
            _cardName.text = cardData.CardName;
            _cardAttack.text = $"ATK: {cardData.Attack}";
            _cardDefense.text = $"DEF: {cardData.Defense}";
            _cardXp.text = $"XP: {cardData.XP}";
            _cardType.text = $"{cardData.CardType}";
        }

        private void SetPosition(Vector3 position)
        {
            _popUp.transform.position = position;
        }
    }
}