using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeBtn;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake() =>
            _closeBtn?.onClick.AddListener(() => Destroy(gameObject));
    }
}