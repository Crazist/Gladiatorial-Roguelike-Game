using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class XpViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _xpValueText;
    [SerializeField] private TMP_Text _playerLevel;
    
    [SerializeField] private RectTransform _levelProgressImage;

    private PersistentProgressService _persistentProgressService;
    
    private readonly float _maxXp = 100f;

    [Inject]
    private void Inject(PersistentProgressService persistentProgressService)
    {
        _persistentProgressService = persistentProgressService;
        
        _persistentProgressService.PlayerProgress.Profile.OnExpChanged += OnExpChanged;
        _persistentProgressService.PlayerProgress.Profile.OnLevelChanged += UpdateLevelValueText;
    }

    private void Start()
    {
        UpdateXpValueText();
        UpdateXpProgress();
        UpdateLevelValueText();
    }

    private void OnDestroy()
    {
        _persistentProgressService.PlayerProgress.Profile.OnExpChanged -= OnExpChanged;
        _persistentProgressService.PlayerProgress.Profile.OnLevelChanged -= UpdateLevelValueText;
    }

    private void OnExpChanged()
    {
        UpdateXpValueText();
        UpdateXpProgress();
    }

    private void UpdateXpValueText()
    {
        _xpValueText.text = $"XP: {_persistentProgressService.PlayerProgress.Profile.Exp} / {_maxXp}";
    }
    private void UpdateLevelValueText()
    {
        _playerLevel.text = $"Player Level: {_persistentProgressService.PlayerProgress.Profile.Level}";
    }
    private void UpdateXpProgress()
    {
        float normalizedValue = _persistentProgressService.PlayerProgress.Profile.Exp / _maxXp;
        _levelProgressImage.localScale = new Vector3(normalizedValue, 1, 1);
    }
}