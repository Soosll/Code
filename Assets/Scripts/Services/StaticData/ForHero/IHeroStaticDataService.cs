using StaticData.Player;

namespace Services.StaticData.ForHero
{
    public interface IHeroStaticDataService
    {
        void Load();
        HeroStaticData ForHero();
    }
}