using Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeveloperSettings : MonoBehaviour
{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button _invisibleButton;
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private TMP_Text _xpValueText;
    
    private PersistentProgressService _persistentProgress;

    [Inject]
    private void Inject(PersistentProgressService persistentProgress)
    {
        _persistentProgress = persistentProgress;
        _persistentProgress.PlayerProgress.CurrentRun.OnExpChange += OnLevelChanged;
    }

    private void Start()
    {
        settingsPanel.SetActive(false);

        _invisibleButton.onClick.AddListener(ToggleSettingsPanel);

        _xpSlider.onValueChanged.AddListener(OnXpSliderValueChanged);

        _xpSlider.maxValue = 20;
        _xpSlider.value = _persistentProgress.PlayerProgress.CurrentRun.Level;

        UpdateXpValueText();
    }

    private void OnDestroy() => 
        _persistentProgress.PlayerProgress.CurrentRun.OnExpChange -= OnLevelChanged;

    private void ToggleSettingsPanel() =>
        settingsPanel.SetActive(!settingsPanel.activeSelf);

    private void OnXpSliderValueChanged(float value)
    {
        _persistentProgress.PlayerProgress.CurrentRun.Level = (int)value;
        UpdateXpValueText();
    }

    private void OnLevelChanged(float newXp)
    {
        _xpSlider.value = newXp;
        UpdateXpValueText();
    }

    private void UpdateXpValueText() =>
        _xpValueText.text = $"XP: {_xpSlider.value}";
#endif
}