using System;
using Zenject;

namespace Nightmare
{
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

        public void Register<T>(Action<T> callBackAction) { }

    #endregion

    #region Private Methods

        private void Post(DomainEvent domainEvent)
        {
            signalBus.TryFire(domainEvent);
        }

    #endregion
    }
}