using System;
using System.Collections.Generic;
using Infrastructure.StateMachineForGame.States;

namespace Infrastructure.StateMachineForGame
{
    public class GameStateMachine
    {
        private IExitableState _currentState;

        private Dictionary<Type, IExitableState> _states;

        public void InitStates(BootstrapState bootstrapState, LoadLevelState loadLevelState, GameLoopState gameLoopState)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(GameLoopState)] = gameLoopState,
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            
            TState state = _states[typeof(TState)] as TState;

            _currentState = state;
            
            return state;
        }
    }
}