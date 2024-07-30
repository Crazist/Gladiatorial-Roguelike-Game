using Data.Cards;
using Logic.Enteties;
using Logic.Entities;
using TMPro;
using UnityEngine;

namespace UI.Popup
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

        [Header("Popup Position Settings")]
        [SerializeField] private Vector3 _offsetAbove = new Vector3(0, 50, 0);
        [SerializeField] private Vector3 _offsetBeside = new Vector3(50, 0, 0);

        [SerializeField] private bool _positionAbove = true;

        public void Show(Vector3 position, Card card)
        {
            SetPosition(position);
            UpdateData(card);

            gameObject.SetActive(true);
        }

        private void Start() =>
            Hide();

        public void Hide() =>
            gameObject.SetActive(false);

        private void UpdateData(Card card)
        {
            _cardName.text = card.CardData.CardName;
            _cardType.text = card.CardData.CardType.ToString();

            if (card is UnitCard unitCard)
            {
                SetUnitCard(unitCard);
            }
            else if (card is SpecialCard specialCard)
            {
                SetSpecialCard(specialCard);
            }
        }

        private void SetSpecialCard(SpecialCard specialCard)
        {
            _cardEffect.text = $"Effect: {specialCard.CardData.SpecialData.SpecialEffect}";

            SetCanvasGroupVisibility(_specialGroup, true);
            SetCanvasGroupVisibility(_unitGroup, false);
        }

        private void SetUnitCard(UnitCard unitCard)
        {
            _cardAttack.text = $"ATK: {unitCard.Attack}";
            _cardDefense.text = $"DEF: {unitCard.Defense}";
            _cardXp.text = $"XP: {unitCard.XP}";

            SetCanvasGroupVisibility(_unitGroup, true);
            SetCanvasGroupVisibility(_specialGroup, false);
        }

        private void SetPosition(Vector3 position)
        {
            Vector3 offset = _positionAbove ? _offsetAbove : _offsetBeside;
            _popUp.transform.position = position + offset;
        }

        private void SetCanvasGroupVisibility(CanvasGroup canvasGroup, bool isVisible)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }
}
