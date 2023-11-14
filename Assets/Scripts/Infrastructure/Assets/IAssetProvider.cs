using UnityEngine;

namespace Infrastructure.Assets
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string assetPath);
        GameObject Instantiate(string assetPath, Transform at);
    }
}