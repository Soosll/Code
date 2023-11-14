using Handlers.HeroHandler;
using Infrastructure.Factories.UI;
using Services.InputService;
using SimpleInputNamespace;
using UnityEngine.UI;

namespace Infrastructure.Factories.InputFactory
{
    public class InputFactory
    {
        private readonly IInputService _inputService;
        
        private readonly IHUDFactory _hudFactory;
        private readonly IHeroHandler _heroHandler;

        public InputFactory(IInputService inputService, IHUDFactory hudFactory)
        {
            _inputService = inputService;
            _hudFactory = hudFactory;
            
            _hudFactory.OnPCHUDCreated += InitKeyBoardInput;
            _hudFactory.OnPhoneHUDCreated += InitTouchInput;
        }

        private void InitKeyBoardInput() => 
            SetInput(new KeyBoardInput());

        public void InitTouchInput(Joystick joystick, Button jumpButton) => 
            SetInput(new TouchInput(joystick, jumpButton));

        private void SetInput(IInput input) => 
            _inputService.InitInput(input);
    }
}