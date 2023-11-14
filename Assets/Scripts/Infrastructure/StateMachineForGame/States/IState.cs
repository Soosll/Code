namespace Infrastructure.StateMachineForGame.States
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IPayLoadState<TPayLoad> : IExitableState
    {
        public void Enter(TPayLoad payLoad);
    }
}