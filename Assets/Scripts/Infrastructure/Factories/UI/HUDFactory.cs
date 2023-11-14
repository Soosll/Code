using System;
using Handlers.HeroHandler;
using Infrastructure.Assets;
using Infrastructure.Factories.InputFactory;
using Infrastructure.ResourcesPath;
using Infrastructure.Yandex;
using SimpleInputNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factories.UI
{
    public class HUDFactory : IHUDFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDeviceChecker _deviceChecker;

        public event Action OnPCHUDCreated;
        public event Action<Joystick, Button> OnPhoneHUDCreated;

        public HUDFactory(IAssetProvider assetProvider, IDeviceChecker deviceChecker)
        {
            _assetProvider = assetProvider;
            _deviceChecker = deviceChecker;
        }

        public GameObject CreateHUD()
        {
            return CreatePCHUD();
            if (!_deviceChecker.IsPC)
            {
               return CreatePhoneHUD();
            }
        }

        private GameObject CreatePCHUD()
        {
            var generalHUD = _assetProvider.Instantiate(AssetPath.HUDPath);
            OnPCHUDCreated?.Invoke();
            return generalHUD;
        }

        private GameObject CreatePhoneHUD()
        {
            var phoneHUD = _assetProvider.Instantiate(AssetPath.PhoneHUDPath);
            var joystick = phoneHUD.GetComponentInChildren<Joystick>();
            var jumpButton = phoneHUD.GetComponentInChildren<Button>();
            OnPhoneHUDCreated?.Invoke(joystick, jumpButton);

            return phoneHUD;
        }
    }
}