using UnityEngine;

namespace Infrastructure.Factories.HeroFactory
{
    public interface IHeroFactory
    {
        GameObject CreateHero(Transform parent);
    }
}