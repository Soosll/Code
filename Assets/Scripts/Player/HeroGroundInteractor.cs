using System;
using UnityEngine;

namespace Player
{
    public class HeroGroundInteractor : MonoBehaviour
    {
        [SerializeField] private GameObject _shadowObject;

        private GroundChecker _groundChecker;
        private ShadowShower _shadowShower;

        public bool OnGround { get; private set; }

        private void Start()
        {
            _groundChecker = new GroundChecker();
            _shadowShower = new ShadowShower(_shadowObject);
        }

        private void Update()
        {
            var currentPosition = transform.position;
            
            OnGround = _groundChecker.CheckGround(currentPosition);

            _shadowShower.Show(_groundChecker.GetDistanceToGround(currentPosition));
        }
    }
}