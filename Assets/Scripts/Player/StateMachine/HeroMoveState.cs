using Services.InputService;
using UnityEngine;

namespace Player.StateMachine
{
    public class HeroMoveState : HeroState
    {
        private readonly Hero _hero;
        private readonly HeroAnimator _heroAnimator;
        private readonly Rigidbody _rigidbody;
        private readonly float _runSpeed;

        private readonly IInputService _inputService;

        public HeroMoveState(Hero hero, HeroAnimator heroAnimator, Rigidbody rigidbody, float runSpeed,
            IInputService inputService)
        {
            _hero = hero;
            _heroAnimator = heroAnimator;
            _rigidbody = rigidbody;
            _runSpeed = runSpeed;
            _inputService = inputService;
        }

        public override void Enter()
        {
            _heroAnimator.PlayRun();
            Debug.Log("Вошел в состояние бега");
        }

        public override void Update()
        {
            if(!_inputService.CheckInput())
                _hero.ChangeState<HeroIdleState>();
        }

        public override void FixedUpdate()
        {
            _rigidbody.velocity = _inputService.Direction.normalized * _runSpeed;
        }

        public override void Exit()
        {
        }
    }
}