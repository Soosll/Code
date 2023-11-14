using Extensions;
using UnityEngine;

namespace Player
{
    public class ShadowShower
    {
        private const float ShadowYOffset = 0.1f;
        
        private readonly GameObject _shadow;

        public ShadowShower(GameObject shadow) => 
            _shadow = shadow;

        public void Show(Vector3 shadowPoint)
        {
            _shadow.transform.Activate();

            _shadow.transform.position = new Vector3(shadowPoint.x, shadowPoint.y + ShadowYOffset, shadowPoint.z);
        }

        public void Hide() => 
            _shadow.transform.Diactivate();
    }
}