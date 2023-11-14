using Infrastructure.General;

namespace Handlers.CoroutineRunnerHandler
{
    public interface ICoroutineRunnerHandler
    {
        public ICoroutineRunner CoroutineRunner { get; set; }
    }
}