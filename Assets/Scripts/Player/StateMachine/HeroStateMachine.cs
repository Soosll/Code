using System;
using System.Collections.Generic;
using Infrastructure.StateMachineForGame;
using Services.InputService;
using UnityEngine;

namespace Player.StateMachine
{
    public class HeroStateMachine
    {
        private readonly IInputService _inputService;
        private readonly Hero _hero;
        public HeroState CurrentState;


        private readonly HeroIdleState _idleState;
        private readonly HeroMoveState _moveState;
        private readonly HeroJumpState _jumpState;
        private readonly HeroDieState _dieState;
        private readonly HeroWinState _winState;
        
        private Dictionary<Type, HeroState> _states;

        public HeroStateMachine(Hero hero, HeroAnimator heroAnimator, Rigidbody rigidbody, HeroStats stats,
            IInputService inputService, HeroGroundInteractor heroGroundInteractor)
        {
            _idleState = new HeroIdleState(hero, heroAnimator, rigidbody, inputService);
            _moveState = new HeroMoveState(hero, heroAnimator, rigidbody, stats.MoveSpeed, inputService);
            _jumpState = new HeroJumpState(hero, heroAnimator, rigidbody, stats.JumpHeight, heroGroundInteractor);
            _dieState = new HeroDieState();
            _winState = new HeroWinState();
        }
        
        public void AddStates()
        {
            _states = new Dictionary<Type, HeroState>();
            
            _states.Add(typeof(HeroIdleState), _idleState);
            _states.Add(typeof(HeroMoveState), _moveState);
            _states.Add(typeof(HeroJumpState), _jumpState);
            _states.Add(typeof(HeroDieState), _dieState);
            _states.Add(typeof(HeroWinState), _winState);
        }
        
        public void EnterState<TState>() where TState : HeroState
        {
            CurrentState?.Exit();
            var state = _states[typeof(TState)];
            CurrentState = state;
            state.Enter();
        }
    }
}