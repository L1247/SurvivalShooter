using Nightmare;
using NUnit.Framework;
using Zenject;

namespace MainTests
{
    public class DomainEventBusTests : ZenjectUnitTestFixture
    {
    #region Setup/Teardown Methods

        [SetUp]
        public void SetUp()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<DomainEventBus>().AsSingle();
        }

    #endregion

    #region Test Methods

        [Test]
        public void CreateDomainEventBus()
        {
            var domainEventBus = Container.Resolve<DomainEventBus>();
            var callBacks      = domainEventBus.CallBacks;
            Assert.NotNull(callBacks);
        }

        [Test]
        public void Register()
        {
            var domainEventBus = Container.Resolve<DomainEventBus>();
            domainEventBus.Register<FakeDomainEvent>(OnFakeDomainEvent);
            var callBacks = domainEventBus.CallBacks;
            var type      = typeof(FakeDomainEvent);
            Assert.AreEqual(true , callBacks.ContainsKey(type));
            var actions = callBacks[type];
            Assert.AreEqual(1 , actions.Count);
        }

    #endregion

    #region Private Methods

        private void OnFakeDomainEvent(FakeDomainEvent fakeDomainEvent) { }

    #endregion
    }

    public class FakeDomainEvent : DomainEvent { }
}