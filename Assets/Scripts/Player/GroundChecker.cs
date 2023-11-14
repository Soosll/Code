using ForGame;
using UnityEngine;

namespace Player
{
    public class GroundChecker
    {
        public Vector3 GroundHitPoint { get; private set; }

        public bool CheckGround(Vector3 from)
        {
            RaycastHit hit;

            if (Physics.Raycast(GetRay(from), out hit, 0.1f))
            {
                if (hit.collider.gameObject.TryGetComponent(out Ground ground))
                {
                    GroundHitPoint = hit.collider.gameObject.transform.position;

                    return true;
                }
            }

            return false;
        }

        public Vector3 GetDistanceToGround(Vector3 from)
        {
            RaycastHit hit;

            if (Physics.Raycast(GetRay(from), out hit, 20))
            {
                var hittedGameObject = hit.collider.gameObject;

                if (hittedGameObject.TryGetComponent(out Ground ground))
                {
                    return hittedGameObject.transform.position;
                }
            }

            return Vector3.zero;
        }

        private Ray GetRay(Vector3 from)
        {
            Ray ray = new Ray(from, Vector3.down);

            return ray;
        }
    }
}