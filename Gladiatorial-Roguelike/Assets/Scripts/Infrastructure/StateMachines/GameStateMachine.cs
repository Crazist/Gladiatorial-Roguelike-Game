using System;
using System.Collections.Generic;
using Infrastructure.States;
using Zenject;

namespace Infrastructure.StateMachines
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;

        private DiContainer _diContainer;
       
        private IExitableState _activeState;

        [Inject]
        private void Inject(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _states = new Dictionary<Type, IExitableState>();

            InitStates();
        }

        public void InitStates()
        {
            _states[typeof(BootstrapState)] = _diContainer.Instantiate<BootstrapState>();
            _states[typeof(LoadProgressState)] = _diContainer.Instantiate<LoadProgressState>();
            _states[typeof(LoadLevelState)] = _diContainer.Instantiate<LoadLevelState>();
            _states[typeof(MenuState)] = _diContainer.Instantiate<MenuState>();
            _states[typeof(GameLoopState)] = _diContainer.Instantiate<GameLoopState>();
            _states[typeof(EndGameState)] = _diContainer.Instantiate<EndGameState>();
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