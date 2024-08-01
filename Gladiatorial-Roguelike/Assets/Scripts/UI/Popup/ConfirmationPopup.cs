using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class ConfirmationPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageText;
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

        public void Show(string message, Action onConfirm, Action onCancel = null)
        {
            _messageText.text = message;
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
}