using UnityEngine;

namespace Services.InputService
{
    public class InputService : IInputService
    {
        public bool IsInputDisable { get; set; }
        public IInput Input { get; set; }
        public Vector3 Direction => Input.DirectionAxis;

        public void InitInput(IInput input)
        {
            Input = input;
        }

        public bool CheckInput() => 
            Input.DirectionAxis != Vector3.zero;

        public void Tick()
        {
            if(IsInputDisable)
                return;
            
            Input.UpdateInput(); 
        }
    }
}