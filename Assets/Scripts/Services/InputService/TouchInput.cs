using System;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Services.InputService
{
    public class TouchInput : IInput
    {
        private readonly Joystick _joystick;
        private readonly Button _jumpButton;
        
        public Vector3 DirectionAxis { get; set; }
        
        public event Action OnJumpButtonClicked;

        public TouchInput(Joystick joystick, Button jumpButton)
        {
            _joystick = joystick;
            _jumpButton = jumpButton;
            
            _jumpButton.onClick.AddListener(InvokeJumpEvent);
        }
        
        public void UpdateInput() => 
            DirectionAxis = new Vector3(_joystick.xAxis.value, 0, _joystick.yAxis.value);

        private void InvokeJumpEvent() => 
            OnJumpButtonClicked?.Invoke();
    }
}