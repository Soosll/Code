using Infrastructure.ResourcesPath;
using StaticData.Player;
using UnityEngine;

namespace Services.StaticData.ForHero
{
    public class HeroStaticDataService : IHeroStaticDataService
    {
        private HeroStaticData _heroStaticData;

        public void Load()
        {
            _heroStaticData = Resources.Load<HeroStaticData>(StaticDataPath.HeroStaticDataPath);
            Debug.Log($"Создал статик дату для героя = {_heroStaticData}");
        }

        public HeroStaticData ForHero() => 
            _heroStaticData;
    }
}