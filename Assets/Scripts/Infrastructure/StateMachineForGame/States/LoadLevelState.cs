using Infrastructure.Factories.HeroFactory;
using Infrastructure.Factories.UI;
using Infrastructure.General;
using UnityEngine;

namespace Infrastructure.StateMachineForGame.States
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string PlayerSpawnPointTag = "SpawnPoint";
        
        private readonly SceneLoader _sceneLoader;
        
        private readonly IHeroFactory _heroFactory;
        private readonly IHUDFactory _hudFactory;

        public LoadLevelState(SceneLoader sceneLoader, IHeroFactory heroFactory, IHUDFactory hudFactory)
        {
            _sceneLoader = sceneLoader;
            _heroFactory = heroFactory;
            _hudFactory = hudFactory;
        }

        public void Enter(string payLoad)
        {
            _sceneLoader.Load(payLoad, InitGameWorld);
        }

        public void Exit()
        {
        }

        private void InitGameWorld()
        {
            _hudFactory.CreateHUD();
            
            var spawnPoint = GameObject.FindWithTag(PlayerSpawnPointTag).transform;

            var hero = _heroFactory.CreateHero(spawnPoint);
            
            Debug.Log("Игровой мир создан!");
        }
    }
}