using Infrastructure.General;

namespace Handlers.CoroutineRunnerHandler
{
    public class CoroutineRunnerHandler : ICoroutineRunnerHandler
    {
        public ICoroutineRunner CoroutineRunner { get; set; }
    }
}