using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Unity.VisualScripting;
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

        [Inject]
        public void Inject(DeckService deckService, SceneLoader sceneLoader,
            LoadingCurtain curtain)
        {
            _curtain = curtain;
            _deckService = deckService;
            _sceneLoader = sceneLoader;
        }

        public void InitStates()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), new BootstrapState(this, _sceneLoader)},
                { typeof(LoadLevelState), new LoadLevelState(this, _deckService, _sceneLoader, _curtain)},
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