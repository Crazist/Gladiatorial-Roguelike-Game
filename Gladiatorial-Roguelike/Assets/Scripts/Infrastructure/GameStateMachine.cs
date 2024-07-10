using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI.Factory;
using Zenject;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
      
        private IExitableState _activeState;
        private SceneLoader _sceneLoader;
        private DeckService _deckService;
        private LoadingCurtain _curtain;
        private UIFactory _uiFactory;
        private PersistentProgressService _progressService;
        private SaveLoadService _saveLoadService;

        [Inject]
        public void Inject(DeckService deckService, SceneLoader sceneLoader,
            LoadingCurtain curtain, UIFactory uiFactory, PersistentProgressService progressService,
            SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _uiFactory = uiFactory;
            _curtain = curtain;
            _deckService = deckService;
            _sceneLoader = sceneLoader;
        }

        public void InitStates()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, _sceneLoader)},
                { typeof(LoadProgressState), new LoadProgressState(this, _progressService, _saveLoadService)},
                { typeof(LoadLevelState), new LoadLevelState(this, _deckService, _sceneLoader, _curtain, _uiFactory)},
                { typeof(GameLoopState), new GameLoopState()}
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
             _states[typeof(TState)] as TState;
        
    }
}