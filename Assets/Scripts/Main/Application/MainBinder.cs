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
        Container.DeclareSignal<PlayerCreated>();
        Container.DeclareSignal<PlayerDead>();
        Container.DeclareSignal<PlayerTookDamage>();

        // EventHandler
        Container.Bind<PlayerViewEventHandler>().AsSingle().NonLazy();
    }

#endregion
}