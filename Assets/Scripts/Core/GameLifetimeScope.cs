using Core.Services.InputService;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IInputService, InputService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}