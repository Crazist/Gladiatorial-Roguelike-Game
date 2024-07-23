using Infrastructure.Services;
using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Elements
{
    public class AttackAndDefence : MonoBehaviour
    {
        [SerializeField] private RectTransform _attackZone;
        [SerializeField] private RectTransform _defenceZone;
        [SerializeField] private GameObject _defenseShield;
        [SerializeField] private LineRendererUi _lineRendererUi;

        private CardView _cardView;
        private CardInteractionHandler _cardInteractionHandler;
        private CanvasService _canvasService;
        private Transform _originalParent;
        private bool _isDragging;

        [Inject]
        public void Construct(CanvasService canvasService)
        {
            _canvasService = canvasService;
        }

        public void Initialize(CardView cardView, CardInteractionHandler cardInteractionHandler)
        {
            _cardInteractionHandler = cardInteractionHandler;
            _cardView = cardView;

            _cardInteractionHandler.OnCardClick += HandleCardClick;
            _cardInteractionHandler.OnBeginDragAction += HandleBeginDrag;
            _cardInteractionHandler.OnDragAction += HandleDrag;
            _cardInteractionHandler.OnEndDragAction += HandleEndDrag;

            _lineRendererUi.SetLineActive(false);
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
                _lineRendererUi.SetLineActive(true);
                _originalParent = _lineRendererUi.transform.parent;
                _canvasService.MoveToOverlay(_lineRendererUi.GetComponent<RectTransform>());
                _lineRendererUi.CreateLine(_attackZone.position, Input.mousePosition, Color.red);
                EnableDefenseShield(false);
            }
        }

        private void HandleDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDragging)
            {
                _lineRendererUi.CreateLine(_attackZone.position, Input.mousePosition, Color.red);
            }
        }

        private void HandleEndDrag(CardView cardView, PointerEventData eventData)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _lineRendererUi.SetLineActive(false);
                _canvasService.MoveBack(_lineRendererUi.GetComponent<RectTransform>(), _originalParent);

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
