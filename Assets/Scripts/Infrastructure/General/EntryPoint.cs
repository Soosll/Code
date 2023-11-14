using Handlers.CoroutineRunnerHandler;
using UnityEngine;
using Zenject;

namespace Infrastructure.General
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private ICoroutineRunnerHandler _coroutineRunnerHandler;

        [Inject]
        public void Construct(Game game, ICoroutineRunnerHandler coroutineRunnerHandler)
        {
            _game = game;
            _coroutineRunnerHandler = coroutineRunnerHandler;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            _coroutineRunnerHandler.CoroutineRunner = this;
            
            _game.InitStateMachine();
            _game.RunStateMachine();
        }
    }
}