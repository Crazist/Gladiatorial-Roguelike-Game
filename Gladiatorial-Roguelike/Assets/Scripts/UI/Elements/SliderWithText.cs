using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class SliderWithText : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _valueText;

    [SerializeField] private int _maxValue;

    public void Initialize(float startValue, UnityAction<float> onValueChangedAction)
    {
        _slider.value = startValue;
        _slider.maxValue = _maxValue;
        _valueText.text = _name + "  " + startValue;
        _slider.onValueChanged.AddListener(onValueChangedAction);
        _slider.onValueChanged.AddListener(value => _valueText.text = _name + "  " + value.ToString("0"));
    }
}