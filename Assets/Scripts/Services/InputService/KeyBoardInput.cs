using System;
using Player;
using Player.StateMachine;
using UnityEngine;

namespace Services.InputService
{
    public class KeyBoardInput : IInput
    {
        private readonly Hero _hero;
        public Vector3 DirectionAxis { get; set; }

        public event Action OnJumpButtonClicked;

        public void UpdateInput()
        {
            float _horizontalInput = Input.GetAxisRaw("Horizontal");
            float _verticalInput = Input.GetAxisRaw("Vertical");

            DirectionAxis = new Vector3(_horizontalInput, 0, _verticalInput);

            if (Input.GetKeyDown(KeyCode.Space))
                OnJumpButtonClicked?.Invoke();
        }
    }
}