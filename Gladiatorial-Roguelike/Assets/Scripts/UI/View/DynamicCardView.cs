using Logic.Enteties;
using Logic.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class DynamicCardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private Image _fill;
        [SerializeField] private GameObject _hpBar;
        [SerializeField] private Image _heartIcon;
        
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
       
        private UnitCard _unitCard;
        private int _maxHp;
        private SpecialCard _specialCard;

        public void Initialize(UnitCard unitCard)
        {
            _unitCard = unitCard;
            _maxHp = _unitCard.CardData.UnitData.Hp;
            
            UpdateHp();
            
            _hp.gameObject.SetActive(true);
            _hpBar.gameObject.SetActive(true);
        }

        public void Initialize(SpecialCard specialCard)
        {
            _specialCard = specialCard;
            
            _hp.gameObject.SetActive(false);
            _hpBar.gameObject.SetActive(false);
        }

        public void HideStats(bool hide)
        {
            _hp.gameObject.SetActive(!hide);
            _hpBar.gameObject.SetActive(!hide);
        }

        public UnitCard GetConcreteTCard() => 
            _unitCard;

        public void UpdateCard()
        {
            if (_unitCard != null)
            {
                UpdateHp();
            }
            else if (_specialCard != null)
            {
                UpdateSpecialCard();
            }
        }

        private void UpdateHp()
        {
            int currentHp = _unitCard.Hp;

            if (currentHp > _maxHp)
            {
                _maxHp = currentHp;
            }

            _hp.text = currentHp.ToString() + " HP";

            float fillPercentage = (float)currentHp / _maxHp;
            _fill.rectTransform.anchorMax = new Vector2(fillPercentage, _fill.rectTransform.anchorMax.y);

            if (currentHp <= 0)
            {
                _heartIcon.sprite = _emptyHeartSprite;
            }
            else
            {
                _heartIcon.sprite = _fullHeartSprite;
            }
        }

        private void UpdateSpecialCard()
        {
            // Логика обновления для SpecialCard
            // Например, обновление специальных эффектов
        }
    }
}
