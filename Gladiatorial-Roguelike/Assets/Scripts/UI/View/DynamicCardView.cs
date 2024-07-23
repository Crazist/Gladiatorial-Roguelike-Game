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

        public void Initialize(UnitCard unitCard)
        {
            _unitCard = unitCard;
            
            UpdateHp();
            
            _hp.gameObject.SetActive(true);
            _hpBar.gameObject.SetActive(true);
        }

        public void Initialize(SpecialCard specialCard)
        {
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

        public void UpdateHp()
        {
            int currentHp = _unitCard.Hp;
            int maxHp = _unitCard.CardData.UnitData.Hp;

            _hp.text = currentHp.ToString() + " HP";
            _fill.fillAmount = (float)currentHp / maxHp;

            if (currentHp <= 0)
            {
                _heartIcon.sprite = _emptyHeartSprite;
            }
            else
            {
                _heartIcon.sprite = _fullHeartSprite;
            }
        }
    }
}