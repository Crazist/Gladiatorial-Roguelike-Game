using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Infrastructure.Services;
using Infrastructure.Services.BattleServices;

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
        private bool _isDragging;
        private Transform _originalParent;
        private AttackService _attackService;
        private TableService _tableService;

        [Inject]
        public void Construct(CanvasService canvasService)
        {
            _canvasService = canvasService;
        }

        public void Initialize(CardView cardView, CardInteractionHandler cardInteractionHandler,
            AttackService attackService, TableService tableService)
        {
            _tableService = tableService;
            _attackService = attackService;
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
            if (_cardView.State == CardState.OnTable &&
                RectTransformUtility.RectangleContainsScreenPoint(_defenceZone, Input.mousePosition))
            {
                EnableDefenseShield(true);
            }
        }

        private void HandleBeginDrag(CardView cardView, PointerEventData eventData)
        {
            if (_cardView.State == CardState.OnTable &&
                RectTransformUtility.RectangleContainsScreenPoint(_attackZone, eventData.position))
            {
                _isDragging = true;
                _lineRendererUi.SetLineActive(true);
                _originalParent = _lineRendererUi.transform.parent;
                _canvasService.MoveToOverlay(_lineRendererUi.GetRectTransform());
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
                _canvasService.MoveBack(_lineRendererUi.GetRectTransform(), _originalParent);

                if (TryGetTargetCard(eventData, out CardView targetCardView))
                {
                    Debug.Log("Attack action confirmed.");
                    // Add attack logic here
                    AddAttack(cardView, targetCardView);
                }
            }
        }

        private bool TryGetTargetCard(PointerEventData eventData, out CardView targetCardView)
        {
            targetCardView = null;
            Vector2 screenPoint = eventData.position;

            foreach (var enemyCardView in _tableService.GetEnemyTableViews())
            {
                RectTransform enemyRectTransform = enemyCardView.GetRectTransform();
                if (RectTransformUtility.RectangleContainsScreenPoint(enemyRectTransform, screenPoint,
                        eventData.pressEventCamera))
                {
                    targetCardView = enemyCardView;
                    return true;
                }
            }

            return false;
        }

        private void AddAttack(CardView attacker, CardView defender)
        {
            _attackService.AddAttack(attacker, defender);
        }

        private void EnableDefenseShield(bool enable) => _defenseShield.SetActive(enable);
    }
}