using Infrastructure.StateMachineForGame;
using Infrastructure.StateMachineForGame.States;
using Zenject;

namespace Installers
{
    public class StatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
    }
}