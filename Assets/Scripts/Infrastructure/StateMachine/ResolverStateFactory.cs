using Infrastructure.StateMachine;
using VContainer;

namespace Infrastructure
{
    public class ResolverStateFactory : IStateFactory
    {
        private readonly IObjectResolver _resolver;

        public ResolverStateFactory(IObjectResolver resolver) => _resolver = resolver;

        public TState Create<TState>() where TState : class, IExitableState
        {
            return _resolver.Resolve<TState>();
        }
    }

    public interface IStateFactory
    {
        TState Create<TState>() where TState : class, IExitableState;
    }
}