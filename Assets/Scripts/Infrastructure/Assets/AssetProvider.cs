using UnityEngine;
using Zenject;

namespace Infrastructure.Assets
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IInstantiator _instantiator;

        public AssetProvider(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public GameObject Instantiate(string assetPath)
        {
            var gameObject = Resources.Load(assetPath);

            return _instantiator.InstantiatePrefab(gameObject);
        }

        public GameObject Instantiate(string assetPath, Transform at)
        {
            var gameObject = Resources.Load(assetPath);

            return _instantiator.InstantiatePrefab(gameObject, at);
        }
    }
}