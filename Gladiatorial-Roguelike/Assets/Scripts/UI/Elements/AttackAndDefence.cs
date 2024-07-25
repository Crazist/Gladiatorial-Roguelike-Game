using Logic.Types;
using UI.View;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        private bool _hasShield;
        private bool _shieldBroken;

        public bool HasShield => _hasShield;
        public bool ShieldBroken => _shieldBroken;

        private CardView _cardView;
        private CardInteractionHandler _cardInteractionHandler;
        private CanvasService _canvasService;
        private AttackService _attackService;
        private TableService _tableService;

        private bool _isDragging;

        public void Initialize(CardView cardView, CardInteractionHandler cardInteractionHandler,
            CanvasService canvasService, AttackService attackService, TableService tableService)
        {
            _cardInteractionHandler = cardInteractionHandler;
            _cardView = cardView;
            _canvasService = canvasService;
            _attackService = attackService;
            _tableService = tableService;

            _cardInteractionHandler.OnCardClick += HandleCardClick;
            _cardInteractionHandler.OnBeginDragAction += HandleBeginDrag;
            _cardInteractionHandler.OnDragAction += HandleDrag;
            _cardInteractionHandler.OnEndDragAction += HandleEndDrag;

            _lineRendererUi.SetLineActive(false);
            _hasShield = false;
            _shieldBroken = false;
        }

        public void RemoveLine()
        {
            _lineRendererUi.SetLineActive(false);
            _canvasService.MoveBack(_lineRendererUi.GetRectTransform());
        }

        public void CleanUp()
        {
            EnableDefenseShield(false);
            RemoveLine();
            RemoveAttack();
            _isDragging = false;
            _cardView.SetDraggingState(_isDragging);
            _hasShield = false;
        }

        public void EnableShield()
        {
            if (!_shieldBroken && !_hasShield)
            {
                EnableDefenseShield(true);
                _hasShield = true;
            }
        }

        public void DisableShield()
        {
            EnableDefenseShield(false);
            _hasShield = false;
        }

        public void BreakShield()
        {
            _shieldBroken = true;
            DisableShield();
        }

        public void ResetShieldStatus()
        {
            _shieldBroken = false;
            _hasShield = false;
        }

        private void HandleCardClick(CardView cardView)
        {
            if (CheckIfEnemy()) return;

            if (_cardView.State == CardState.OnTable &&
                RectTransformUtility.RectangleContainsScreenPoint(_defenceZone, Input.mousePosition))
            {
                if (_hasShield || _shieldBroken) return;
                CleanUp();
                EnableDefenseShield(true);
                _hasShield = true;
            }
        }

        private void HandleBeginDrag(CardView cardView, PointerEventData eventData)
        {
            if (CheckIfEnemy()) return;

            if (_cardView.State == CardState.OnTable &&
                RectTransformUtility.RectangleContainsScreenPoint(_attackZone, eventData.position))
            {
                CleanUp();
                _isDragging = true;
                _cardView.SetDraggingState(_isDragging);
                _lineRendererUi.SetLineActive(true);
                _canvasService.MoveToOverlay(_lineRendererUi.GetRectTransform());
                _lineRendererUi.CreateLine(_attackZone.position, Input.mousePosition, Color.red);
                EnableDefenseShield(false);
            }
        }

        private bool CheckIfEnemy() => _cardView.Team == TeamType.Enemy;

        private void HandleDrag(CardView cardView, PointerEventData eventData)
        {
            if (CheckIfEnemy()) return;

            if (_isDragging)
            {
                _lineRendererUi.CreateLine(_attackZone.position, Input.mousePosition, Color.red);
            }
        }

        private void HandleEndDrag(CardView cardView, PointerEventData eventData)
        {
            if (CheckIfEnemy()) return;

            if (_isDragging)
            {
                _isDragging = false;
                _cardView.SetDraggingState(_isDragging);

                if (TryGetTargetCard(eventData, out CardView targetCardView))
                {
                    Debug.Log("Attack action confirmed.");
                    AddAttack(cardView, targetCardView);
                    _lineRendererUi.CreateLine(_attackZone.position, targetCardView.GetRectTransform().position, Color.red);
                }
                else
                {
                    CleanUp();
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
                if (RectTransformUtility.RectangleContainsScreenPoint(enemyRectTransform, screenPoint, eventData.pressEventCamera))
                {
                    targetCardView = enemyCardView;
                    return true;
                }
            }

            return false;
        }

        private void AddAttack(CardView attacker, CardView defender) =>
            _attackService.AddAttack(attacker, defender);

        private void RemoveAttack() => 
            _attackService.RemoveAttack(_cardView);

        private void EnableDefenseShield(bool enable) =>
            _defenseShield.SetActive(enable);
    }
}
