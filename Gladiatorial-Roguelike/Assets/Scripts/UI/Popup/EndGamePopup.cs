using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using Infrastructure.Services.BattleServices;
using Infrastructure.StateMachines;
using Infrastructure.States.BattleStates;

namespace UI.Popup
{
    public class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private Button _confirm;
        [SerializeField] private CanvasGroup _canvasGroup;

        private TurnService _turnService;
        private BattleStateMachine _battleStateMachine;

        [Inject]
        public void Inject(TurnService turnService, BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
            _turnService = turnService;

            Initialize();
        }

        private void Initialize()
        {
            HidePopup();
            
            _turnService.OnBattleEnd += ShowPopup;
            _confirm.onClick.AddListener(OnConfirm);
        }

        private void OnDisable()
        {
            _turnService.OnBattleEnd -= ShowPopup;
            _confirm.onClick.RemoveListener(OnConfirm);
        }

        private void ShowPopup()
        {
            _canvasGroup.DOFade(1f, 2f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
            gameObject.SetActive(true);
        }

        private void HidePopup()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        private void OnConfirm() => _battleStateMachine.Enter<BattleEndState>();
    }
}