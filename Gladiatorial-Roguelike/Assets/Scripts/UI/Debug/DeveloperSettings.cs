using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Infrastructure.States.BattleStates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Debug
{
    public class DeveloperSettings : MonoBehaviour
    {
        [SerializeField] private List<SliderWithText> _sliders;
        [SerializeField] private Button _openDebugPanelButton;
        [SerializeField] private Button _clearPrefsButton;
        [SerializeField] private Button _fastWin;
        [SerializeField] private Button _fastLose;
        [SerializeField] private GameObject _panel;

        private PersistentProgressService _persistentProgressService;
        private TableService _tableService;
        private TurnService _turnService;
        private BattleStateMachine _battleStateMachine;

        [Inject]
        private void Inject(PersistentProgressService persistentProgressService, TableService tableService,
            TurnService turnService, BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
            _turnService = turnService;
            _tableService = tableService;
            _persistentProgressService = persistentProgressService;
        }

        private void Start()
        {
            _clearPrefsButton.onClick.AddListener(ClearAllPlayerPrefs);
            _openDebugPanelButton.onClick.AddListener(ToggleDebugPanel);
            _fastWin.onClick.AddListener(FastWin);
            _fastLose.onClick.AddListener(FastLose);

            UpdateData();
        }

        private void OnEnable()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (_persistentProgressService == null) return;

            Profile Profile = _persistentProgressService.PlayerProgress.Profile;

            _sliders[0].Initialize(Profile.Exp, value
                => _persistentProgressService.PlayerProgress.Profile.Exp = (int)value);

            _sliders[1].Initialize(Profile.Level, value
                => _persistentProgressService.PlayerProgress.Profile.Level = (int)value);

            _panel.SetActive(false);
        }

        private void ToggleDebugPanel()
        {
            _panel.SetActive(!_panel.activeSelf);
        }

        private void FastWin()
        {
            _tableService.GetEnemyHandViews().Clear();
            _tableService.GetEnemyTableViews().Clear();
            _battleStateMachine.Enter<BattleCalculationState>();
        }
        private void FastLose()
        {
            _tableService.GetPlayerHandViews().Clear();
            _tableService.GetPlayerTableViews().Clear();
            _battleStateMachine.Enter<BattleCalculationState>();
        }
        private void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            _persistentProgressService.PlayerProgress.ClearProgress();

            UpdateData();
        }
    }
}