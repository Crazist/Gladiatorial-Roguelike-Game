using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeBtn;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void OnAwake() =>
            _closeBtn?.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void CleanUp(){}
    }
}