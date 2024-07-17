using System;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string payload = "Main";

        private GameStateMachine _gameStateMachine;
        private PersistentProgressService _persistentProgressService;
        private SaveLoadService _saveLoadService;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, PersistentProgressService persistentProgressService,
            SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(payload);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _persistentProgressService.PlayerProgress = _saveLoadService.LoadProgress() ??
                                                        _persistentProgressService.InitProgress();
    }
}