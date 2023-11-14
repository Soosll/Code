using UnityEngine;

namespace Player.StateMachine
{
    public class HeroJumpState : HeroState
    {
        private readonly Hero _hero;
        private readonly HeroAnimator _heroAnimator;
        private readonly Rigidbody _rigidbody;
        private readonly float _jumpHeight;
        private readonly HeroGroundInteractor _heroGroundInteractor;

        public HeroJumpState(Hero hero, HeroAnimator heroAnimator, Rigidbody rigidbody, float jumpHeight, HeroGroundInteractor heroGroundInteractor)
        {
            _hero = hero;
            _heroAnimator = heroAnimator;
            _rigidbody = rigidbody;
            _jumpHeight = jumpHeight;
            _heroGroundInteractor = heroGroundInteractor;
        }

        public override void Enter()
        {
            _heroAnimator.PlayJump();
            if(!_heroGroundInteractor.OnGround)
                Exit();

            _rigidbody.AddForce(Vector3.up * _jumpHeight);
        }
        
        public override void Exit()
        {
            
        }
    }
}