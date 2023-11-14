using Infrastructure.StateMachineForGame;
using Infrastructure.StateMachineForGame.States;

namespace Infrastructure.General
{
    public class Game
    {
        private GameStateMachine _stateMachine;
        private readonly BootstrapState _bootstrapState;
        private readonly LoadLevelState _loadLevelState;
        private readonly GameLoopState _gameLoopState;

        public Game(GameStateMachine stateMachine, BootstrapState bootstrapState, LoadLevelState loadLevelState, GameLoopState gameLoopState)
        {
            _stateMachine = stateMachine;
            _bootstrapState = bootstrapState;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
        }

        public void InitStateMachine() => 
            _stateMachine.InitStates(_bootstrapState, _loadLevelState, _gameLoopState);

        public void RunStateMachine() => 
            _stateMachine.Enter<BootstrapState>();
    }
}