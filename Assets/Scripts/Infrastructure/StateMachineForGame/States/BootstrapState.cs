using Services.StaticData.ForHero;

namespace Infrastructure.StateMachineForGame.States
{
    public class BootstrapState : IState
    {
        private const string MenuSceneName = "Level 1";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IHeroStaticDataService _heroStaticDataService;

        public BootstrapState(GameStateMachine gameStateMachine, IHeroStaticDataService heroStaticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _heroStaticDataService = heroStaticDataService;
        }

        public void Enter()
        {
            LoadStaticData();

            _gameStateMachine.Enter<LoadLevelState, string>(MenuSceneName);
        }

        public void Exit()
        {
        }

        private void LoadStaticData()
        {
            _heroStaticDataService.Load();
        }
    }
}