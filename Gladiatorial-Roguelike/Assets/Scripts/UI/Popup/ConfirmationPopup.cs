using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    
    private Action _onConfirm;
    private Action _onCancel;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(OnConfirmClicked);
        _cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmClicked);
        _cancelButton.onClick.RemoveListener(OnCancelClicked);
    }

    private void Start() => 
        gameObject.SetActive(false);

    public void Show(Action onConfirm, Action onCancel = null)
    {
        _onConfirm = onConfirm;
        _onCancel = onCancel;
        gameObject.SetActive(true);
    }

    private void OnConfirmClicked()
    {
        _onConfirm?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnCancelClicked()
    {
        _onCancel?.Invoke();
        gameObject.SetActive(false);
    }
}