using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class XpProgressController : MonoBehaviour
{
    [SerializeField] private TMP_Text xpValueText;
    [SerializeField] private Image levelProgressImage;

    private PersistentProgressService _persistentProgressService;
    private float maxXp = 100f;

    [Inject]
    private void Inject(PersistentProgressService persistentProgressService)
    {
        _persistentProgressService = persistentProgressService;
        _persistentProgressService.PlayerProgress.CurrentRun.OnExpChange += OnExpChanged;
    }

    private void Start()
    {
        UpdateXpValueText();
        UpdateLevelProgress();
    }

    private void OnDestroy()
    {
        if (_persistentProgressService != null)
        {
            _persistentProgressService.PlayerProgress.CurrentRun.OnExpChange -= OnExpChanged;
        }
    }

    private void OnExpChanged(float newExp)
    {
        UpdateXpValueText();
        UpdateLevelProgress();
    }

    private void UpdateXpValueText()
    {
        xpValueText.text = $"XP: {_persistentProgressService.PlayerProgress.CurrentRun.Exp}";
    }

    private void UpdateLevelProgress()
    {
        float normalizedValue = _persistentProgressService.PlayerProgress.CurrentRun.Exp / maxXp;
        Vector2 anchoredPosition = levelProgressImage.rectTransform.anchoredPosition;
        anchoredPosition.x = normalizedValue * levelProgressImage.rectTransform.sizeDelta.x;
        levelProgressImage.rectTransform.anchoredPosition = anchoredPosition;
    }
}