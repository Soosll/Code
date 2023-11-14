using Handlers.CoroutineRunnerHandler;
using Handlers.HeroHandler;
using Infrastructure.Assets;
using Infrastructure.Factories.HeroFactory;
using Infrastructure.Factories.InputFactory;
using Infrastructure.Factories.UI;
using Infrastructure.General;
using Infrastructure.Yandex;
using Services.InputService;
using Services.StaticData.ForHero;
using Zenject;

namespace Installers
{
    public class GameLogicInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Game>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IDeviceChecker>().To<DeviceChecker>().AsSingle();
            Container.Bind<IHeroHandler>().To<HeroHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.Bind<ICoroutineRunnerHandler>().To<CoroutineRunnerHandler>().AsSingle();
            
            BindFactories();
            BindStaticDataServices();
        }

        private void BindFactories()
        {
            Container.Bind<InputFactory>().AsSingle().NonLazy();
            Container.Bind<IHUDFactory>().To<HUDFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }

        private void BindStaticDataServices()
        {
            Container.Bind<IHeroStaticDataService>().To<HeroStaticDataService>().AsSingle();
        }
    }
}