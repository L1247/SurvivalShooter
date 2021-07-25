using Nightmare;
using Nightmare.EventHandlers;
using Zenject;

public class MainBinder : MonoInstaller
{
#region Public Methods

    public override void InstallBindings()
    {
        // Events
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<DomainEvent>();
        Container.Bind<IDomainEventBus>().To<DomainEventBus>().AsSingle();

        // EventHandler
        Container.Bind<PlayerViewEventHandler>().AsSingle().NonLazy();
    }

#endregion
}