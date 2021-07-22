using Zenject;

namespace Nightmare
{
    public interface IDomainEventBus
    {
    #region Public Methods

        void PostAll(AggregateRoot aggregateRoot);

    #endregion
    }

    public class DomainEventBus : IDomainEventBus
    {
    #region Private Variables

        private readonly SignalBus signalBus;

    #endregion

    #region Constructor

        [Inject]
        public DomainEventBus(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

    #endregion

    #region Public Methods

        public void PostAll(AggregateRoot aggregateRoot)
        {
            var domainEvents = aggregateRoot.GetDomainEvents();
            foreach (var domainEvent in domainEvents)
                Post(domainEvent);
            aggregateRoot.ClearDomainEvent();
        }

    #endregion

    #region Private Methods

        private void Post(DomainEvent domainEvent)
        {
            signalBus.TryFire(domainEvent);
        }

    #endregion
    }
}