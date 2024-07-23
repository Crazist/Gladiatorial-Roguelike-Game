using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class AttackAndDefence : MonoBehaviour
    {
        [SerializeField] private RectTransform _attackZone;
        [SerializeField] private RectTransform _defenceZone;
        [SerializeField] private GameObject _defenseShield;
        [SerializeField] private LineRenderer _attackLineRenderer;

        private CardView _cardView;
        private CardInteractionHandler _cardInteractionHandler;
        private bool _isDragging;

        public void Initialize(CardView cardView, CardInteractionHandler cardInteractionHandler)
        {
            _cardInteractionHandler = cardInteractionHandler;
            _cardView = cardView;

            _cardInteractionHandler.OnCardClick += HandleCardClick;
            _cardInteractionHandler.OnBeginDragAction += HandleBeginDrag;
            _cardInteractionHandler.OnDragAction += HandleDrag;
            _cardInteractionHandler.OnEndDragAction += HandleEndDrag;

            _attackLineRenderer.positionCount = 2;
            _attackLineRenderer.sortingOrder = 10; // Ensure the line is rendered on top of other UI elements

            if (_attackLineRenderer.material == null)
            {
                _attackLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                _attackLineRenderer.startColor = Color.red;
                _attackLineRenderer.endColor = Color.red;
                _attackLineRenderer.startWidth = 0.1f;
                _attackLineRenderer.endWidth = 0.1f;
            }
        }

        public void CleanUp() => EnableDefenseShield(false);

        private void HandleCardClick(CardView cardView)
        {
            if (_cardView.State == CardState.OnTable && RectTransformUtility.RectangleContainsScreenPoint(_defenceZone, Input.mousePosition))
            {
                EnableDefenseShield(true);
            }
        }

        private void HandleBeginDrag(CardView cardView, PointerEventData eventData)
        {
            if (_cardView.State == CardState.OnTable && RectTransformUtility.RectangleContainsScreenPoint(_attackZone, eventData.position))
            {
                _isDragging = true;
                _attackLineRenderer.gameObject.SetActive(true);
                Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(_attackZone.position.x, _attackZone.position.y, 10));
                _attackLineRenderer.SetPosition(0, worldStartPosition);
                EnableDefenseShield(false);
            }
        }

        private void HandleDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDragging)
            {
                Vector3 worldPosition;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_cardView.GetRectTransform(), eventData.position, eventData.pressEventCamera, out worldPosition);
                worldPosition.z = 0; // Adjust depth if necessary
                _attackLineRenderer.SetPosition(1, worldPosition);
            }
        }

        private void HandleEndDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _attackLineRenderer.gameObject.SetActive(false);

                if (TryGetTargetCard(eventData, out CardView targetCardView))
                {
                    Debug.Log("Attack action confirmed.");
                    // Implement attack logic
                }
            }
        }

        private bool TryGetTargetCard(PointerEventData eventData, out CardView targetCardView)
        {
            targetCardView = null;

            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(eventData.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    targetCardView = hit.collider.GetComponent<CardView>();
                    return targetCardView != null;
                }
            }

            return false;
        }

        private void EnableDefenseShield(bool enable) => _defenseShield.SetActive(enable);
    }
}
