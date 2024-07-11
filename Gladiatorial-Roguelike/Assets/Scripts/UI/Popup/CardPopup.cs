using Logic.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class CardPopup : MonoBehaviour
    {
        [SerializeField] private Text _cardName;
        [SerializeField] private Text _cardAttack;
        [SerializeField] private Text _cardDefense;
        [SerializeField] private Text _cardXp;

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
            _cardXp.text = $"XP: {cardData.Hp}";
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}