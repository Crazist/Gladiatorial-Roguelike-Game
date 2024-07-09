using UI.Service;
using UI.Type;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class OpenWindowBtn : MonoBehaviour
    {
        [SerializeField] private Button _open;
        [SerializeField] private WindowId _windowId;

        private WindowService _windowService;

        [Inject]
        private void Inject(WindowService windowService)
        {
            _windowService = windowService;
        }
        private void Awake()
        {
            _open.onClick.AddListener(Open);
        }

        private void Open()
        {
            _windowService.Open(_windowId);
        }
    }
}