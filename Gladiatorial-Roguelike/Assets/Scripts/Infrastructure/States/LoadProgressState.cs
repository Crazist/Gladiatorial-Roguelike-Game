using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string payload = "Main";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly PersistentProgressService _persistentProgressService;
        private readonly SaveLoadService _saveLoadService;
        
        public LoadProgressState(GameStateMachine gameStateMachine,
            PersistentProgressService persistentProgressService, SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
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
            _persistentProgressService.PlayerProgress = _saveLoadService.LoadProgress() ?? InitProgress();

        private PlayerProgress InitProgress()
        {
            var progress = new PlayerProgress();

            progress.DeckProgress.CurrentDeck = DeckType.None;
            progress.Profile.Exp = 0;
            progress.Profile.Level = 0;

            return progress;
        }
    }
}