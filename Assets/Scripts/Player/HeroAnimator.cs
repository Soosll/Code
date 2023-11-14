using UnityEngine;

namespace Player
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private string _jumpAnimationName;
        [SerializeField] private string _idleAnimationName;
        [SerializeField] private string _runAnimationName;
        [SerializeField] private string _winAnimationName;

        public void PlayIdle() => 
            _animator.CrossFade(_idleAnimationName,0.1f);

        public void PlayRun() => 
            _animator.CrossFade(_runAnimationName,0.1f);

        public void PlayJump() => 
            _animator.CrossFade(_jumpAnimationName,0.1f);

        public void Win() => 
            _animator.CrossFade(_winAnimationName,0.1f);
    }
}