using UnityEngine;
using Zenject;

namespace Services.InputService
{
    public interface IInputService : ITickable
    {
        public bool IsInputDisable { get; set; }
        public IInput Input { get; set; }
        public Vector3 Direction { get; }
        public void InitInput(IInput input);

        public bool CheckInput();
            
    }
}