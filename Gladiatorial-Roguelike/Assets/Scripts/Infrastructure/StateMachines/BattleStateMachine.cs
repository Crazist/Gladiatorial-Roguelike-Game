using System;
using System.Collections.Generic;
using Infrastructure.Services.BattleService;
using Zenject;

namespace Infrastructure.StateMachines
{
    public class BattleStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private DiContainer _diContainer;
        private IExitableState _activeState;
        private TurnService _turnService;

        [Inject]
        private void Inject(DiContainer diContainer, TurnService turnService)
        {
            _turnService = turnService;
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
            OnStateChanged(typeof(TState));
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
            OnStateChanged(typeof(TState));
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

        private void OnStateChanged(Type newState)
        {
            if (newState == typeof(PlayerTurnState))
            {
                _turnService.StartPlayerTurn();
            }
            else if (newState == typeof(EnemyTurnState))
            {
                _turnService.StartEnemyTurn();
            }
        }
    }
}