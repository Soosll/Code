using System;
using UnityEngine;

namespace Services.InputService
{
    public interface IInput
    {
        public Vector3 DirectionAxis { get; set; }

        public event Action OnJumpButtonClicked;
        
        void UpdateInput();
    }
}