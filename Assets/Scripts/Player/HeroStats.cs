namespace Player
{
    public class HeroStats
    {
        public float MoveSpeed { get; private set; }
        public float JumpHeight { get; private set; }

        public HeroStats(float moveSpeed, float jumpHeight)
        {
            MoveSpeed = moveSpeed;
            JumpHeight = jumpHeight;
        }
    }
}