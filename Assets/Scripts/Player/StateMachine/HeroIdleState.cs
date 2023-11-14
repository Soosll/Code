using Services.InputService;
using UnityEngine;

namespace Player.StateMachine
{
    public class HeroIdleState : HeroState
    {
        private readonly Hero _hero;
        private readonly HeroAnimator _heroAnimator;
        private readonly Rigidbody _rigidbody;
        private readonly IInputService _inputService;

        public HeroIdleState(Hero hero, HeroAnimator heroAnimator, Rigidbody rigidbody, IInputService inputService)
        {
            _hero = hero;
            _heroAnimator = heroAnimator;
            _rigidbody = rigidbody;
            _inputService = inputService;
        }

        public override void Enter()
        {
            _rigidbody.velocity = Vector3.zero;
            _heroAnimator.PlayIdle();
        }

        public override void Update()
        {
            if(_inputService.CheckInput())
                _hero.ChangeState<HeroMoveState>();
        }
    }
}