using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure
{
    public class BattleStateMachine
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

        private void InitStates()
        {
            _states[typeof(BattleStartState)] = _diContainer.Instantiate<BattleStartState>();
            _states[typeof(PlayerTurnState)] = _diContainer.Instantiate<PlayerTurnState>();
            _states[typeof(EnemyTurnState)] = _diContainer.Instantiate<EnemyTurnState>();
            _states[typeof(BattleEndState)] = _diContainer.Instantiate<BattleEndState>();
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