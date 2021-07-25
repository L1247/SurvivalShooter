using Nightmare;
using Nightmare.EventHandlers;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace MainTests
{
    public class PlayerViewEventHandlerTests : ZenjectUnitTestFixture
    {
    #region Private Variables

        private PlayerViewEventHandler eventHandler;

    #endregion

    #region Setup/Teardown Methods

        [SetUp]
        public void Setup()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<IDomainEventBus>().FromSubstitute().AsSingle();
            eventHandler = Container.Instantiate<PlayerViewEventHandler>();
        }

    #endregion

    #region Test Methods

        [Test]
        public void Should_Binding_Player_Events()
        {
            var domainEventBus = Container.Resolve<IDomainEventBus>();
            domainEventBus.Received(1).Register<PlayerDead>(eventHandler.OnPlayerDead);
            domainEventBus.Received(1).Register<PlayerTookDamage>(eventHandler.OnPlayerTookDamage);
        }

    #endregion
    }
}