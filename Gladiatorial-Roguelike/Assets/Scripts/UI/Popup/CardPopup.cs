using Logic.Cards;
using Logic.Entities;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class CardPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardName;
        [SerializeField] private TMP_Text _cardAttack;
        [SerializeField] private TMP_Text _cardDefense;
        [SerializeField] private TMP_Text _cardXp;
        [SerializeField] private TMP_Text _cardEffect;
        [SerializeField] private TMP_Text _cardType;
        [SerializeField] private GameObject _popUp;

        [SerializeField] private CanvasGroup _unitGroup;
        [SerializeField] private CanvasGroup _specialGroup;

        public void Show(Vector3 position, Card card)
        {
            SetPosition(position);
            UpdateData(card.CardData);
          
            gameObject.SetActive(true);
        }

        private void Start() =>
            Hide();

        public void Hide() =>
            gameObject.SetActive(false);

        private void UpdateData(CardData cardData)
        {
            _cardName.text = cardData.CardName;
            _cardType.text = cardData.CardType.ToString();

            if (cardData is UnitCardData unitCardData)
            {
                SetUnitCard(unitCardData);
            }
            else if (cardData is SpecialCardData specialCardData)
            {
                SetSpecialCard(specialCardData);
            }
        }

        private void SetSpecialCard(SpecialCardData specialCardData)
        {
            _cardEffect.text = $"Effect: {specialCardData.SpecialEffect}";
          
            SetCanvasGroupVisibility(_specialGroup, true);
            SetCanvasGroupVisibility(_unitGroup, false);
        }

        private void SetUnitCard(UnitCardData unitCardData)
        {
            _cardAttack.text = $"ATK: {unitCardData.Attack}";
            _cardDefense.text = $"DEF: {unitCardData.Defense}";
            _cardXp.text = $"XP: {unitCardData.XP}";
           
            SetCanvasGroupVisibility(_unitGroup, true);
            SetCanvasGroupVisibility(_specialGroup, false);
        }

        private void SetPosition(Vector3 position) =>
            _popUp.transform.position = position;

        private void SetCanvasGroupVisibility(CanvasGroup canvasGroup, bool isVisible)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }
}
