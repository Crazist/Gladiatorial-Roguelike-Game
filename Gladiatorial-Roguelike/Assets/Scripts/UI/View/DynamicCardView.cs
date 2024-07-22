using Logic.Cards;
using Logic.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private Image _hpBar;

    public void Initialize(UnitCard unitCard, UnitCardData unitCardData)
    {
        UpdateHp(unitCard.Hp, unitCardData.Hp);
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

    public void UpdateHp(int currentHp, int maxHp)
    {
        _hp.text = currentHp.ToString() + " HP";
        _hpBar.fillAmount = (float)currentHp / maxHp;
    }
}