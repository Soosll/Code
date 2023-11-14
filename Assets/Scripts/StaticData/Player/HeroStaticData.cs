using UnityEngine;

namespace StaticData.Player
{
    [CreateAssetMenu(menuName = "Create/StaticData/Player", fileName = "Player", order = 51)]
    public class HeroStaticData : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpHeight;

        public float MoveSpeed => _moveSpeed;
        public float JumpHeight => _jumpHeight;
    }
}