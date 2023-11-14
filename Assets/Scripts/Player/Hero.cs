using System;
using Player.StateMachine;
using Services.InputService;
using UnityEngine;

namespace Player
{
    public class Hero : MonoBehaviour
    {
        private HeroStateMachine _stateMachine;
        private HeroStats _stats;
        private HeroAnimator _heroAnimator;
        private Rigidbody _rigidbody;

        private HeroGroundInteractor _heroGroundInteractor;
        
        private IInputService _inputService;

        private void Awake()
        {

        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdate();
        }

        public void InitHeroComponents(HeroStats stats, IInputService inputService)
        {
            _stats = stats;
            _inputService = inputService;
            
            _heroAnimator = GetComponent<HeroAnimator>();
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _heroGroundInteractor = GetComponentInChildren<HeroGroundInteractor>();
            
            Subscribe();
        }

        public void InitStateMachine()
        {
            _stateMachine = new HeroStateMachine(this, _heroAnimator, _rigidbody, _stats, _inputService,_heroGroundInteractor);
            _stateMachine.AddStates();
        }
        
        public void ChangeState<TState>() where TState : HeroState => 
            _stateMachine.EnterState<TState>();

        private void Subscribe()
        {
            _inputService.Input.OnJumpButtonClicked += ChangeState<HeroJumpState>;
        }
    }
}