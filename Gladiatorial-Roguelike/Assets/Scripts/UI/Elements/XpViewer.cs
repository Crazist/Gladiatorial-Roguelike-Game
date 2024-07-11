using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class XpViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _xpValueText;
    [SerializeField] private TMP_Text _playerLevel;
    
    [SerializeField] private Image _levelProgressImage;

    private PersistentProgressService _persistentProgressService;
    
    private readonly float _maxXp = 100f;

    [Inject]
    private void Inject(PersistentProgressService persistentProgressService)
    {
        _persistentProgressService = persistentProgressService;
        
        _persistentProgressService.PlayerProgress.CurrentRun.OnExpChanged += OnExpChanged;
        _persistentProgressService.PlayerProgress.CurrentRun.OnLevelChanged += UpdateLevelValueText;
    }

    private void Start()
    {
        UpdateXpValueText();
        UpdateXpProgress();
        UpdateLevelValueText();
    }

    private void OnDestroy()
    {
        _persistentProgressService.PlayerProgress.CurrentRun.OnExpChanged -= OnExpChanged;
        _persistentProgressService.PlayerProgress.CurrentRun.OnLevelChanged -= UpdateLevelValueText;
    }

    private void OnExpChanged()
    {
        UpdateXpValueText();
        UpdateXpProgress();
    }

    private void UpdateXpValueText()
    {
        _xpValueText.text = $"XP: {_persistentProgressService.PlayerProgress.CurrentRun.Exp} / {_maxXp}";
    }
    private void UpdateLevelValueText()
    {
        _playerLevel.text = $"Player Level: {_persistentProgressService.PlayerProgress.CurrentRun.Level}";
    }
    private void UpdateXpProgress()
    {
        float normalizedValue = _persistentProgressService.PlayerProgress.CurrentRun.Exp / _maxXp;
        Vector2 anchoredPosition = _levelProgressImage.rectTransform.anchoredPosition;
        anchoredPosition.x = normalizedValue * _levelProgressImage.rectTransform.sizeDelta.x;
        _levelProgressImage.rectTransform.anchoredPosition = anchoredPosition;
    }
}