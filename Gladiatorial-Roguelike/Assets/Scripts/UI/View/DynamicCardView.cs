using Logic.Entities;
using TMPro;
using UnityEngine;

public class DynamicCardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp;

    public void Initialize(UnitCard unitCard)
    {
        _hp.text = unitCard.Hp.ToString() + "HP";
        _hp.gameObject.SetActive(true);
    }

    public void Initialize(SpecialCard specialCard) => 
        _hp.gameObject.SetActive(false);

    public void HideStats(bool hide) => 
        _hp.gameObject.SetActive(!hide);
}