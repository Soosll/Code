using System;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factories.UI
{
    public interface IHUDFactory
    {
       public event Action OnPCHUDCreated;
       public event Action<Joystick, Button> OnPhoneHUDCreated;
        GameObject CreateHUD();
    }
}