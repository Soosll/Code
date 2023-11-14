using Infrastructure.Assets;
using Infrastructure.Factories.UI;
using Infrastructure.ResourcesPath;
using Player;
using Player.StateMachine;
using Services.InputService;
using Services.StaticData.ForHero;
using UnityEngine;

namespace Infrastructure.Factories.HeroFactory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IHeroStaticDataService _heroStaticDataService;
        private readonly IInputService _inputService;
        private readonly IHUDFactory _hudFactory;

        public HeroFactory(IAssetProvider assetProvider, IHeroStaticDataService heroStaticDataService,
            IInputService inputService, IHUDFactory hudFactory)
        {
            _assetProvider = assetProvider;
            _heroStaticDataService = heroStaticDataService;
            _inputService = inputService;
            _hudFactory = hudFactory;

            _hudFactory.OnPCHUDCreated += Print;

        }

        public GameObject CreateHero(Transform parent)
        {
            GameObject heroPrefab = _assetProvider.Instantiate(AssetPath.HeroPath, parent);
            Hero hero = heroPrefab.GetComponent<Player.Hero>();

            var heroStaticData = _heroStaticDataService.ForHero();

            HeroStats stats = new HeroStats(heroStaticData.MoveSpeed, heroStaticData.JumpHeight);
            hero.InitHeroComponents(stats, _inputService);
            hero.InitStateMachine();
            hero.ChangeState<HeroIdleState>();

            return null;
        }

        private void Print()
        {
            Debug.Log("Событие создания ПК хада");
        }
    }
}