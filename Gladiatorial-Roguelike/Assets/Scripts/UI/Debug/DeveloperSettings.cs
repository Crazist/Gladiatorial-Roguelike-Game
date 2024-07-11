using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeveloperSettings : MonoBehaviour
{
    [SerializeField] private List<SliderWithText> _sliders;
    [SerializeField] private Button _openDebugPanelButton;
    [SerializeField] private Button _clearPrefsButton;
    [SerializeField] private GameObject _panel;

    private PersistentProgressService _persistentProgressService;

    [Inject]
    private void Inject(PersistentProgressService persistentProgressService)
    {
        _persistentProgressService = persistentProgressService;
    }

    private void Start()
    {
        _clearPrefsButton.onClick.AddListener(ClearAllPlayerPrefs);
        _openDebugPanelButton.onClick.AddListener(ToggleDebugPanel);

        UpdateData();
    }

    private void OnEnable()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if(_persistentProgressService == null) return;
        
        CurrentRun currentRun = _persistentProgressService.PlayerProgress.CurrentRun;

        _sliders[0].Initialize(currentRun.Exp, value 
            => _persistentProgressService.PlayerProgress.CurrentRun.Exp = value);
        
        _sliders[1].Initialize(currentRun.Level, value
            =>  _persistentProgressService.PlayerProgress.CurrentRun.Level = (int)value);

        _panel.SetActive(false);
    }

    private void ToggleDebugPanel()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        _persistentProgressService.ClearProgress();
        UpdateData();
    }
}