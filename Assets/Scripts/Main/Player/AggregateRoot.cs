using System.Collections.Generic;

namespace Nightmare
{
    public class AggregateRoot
    {
    #region Private Variables

        private readonly List<DomainEvent> domainEvents = new List<DomainEvent>();

    #endregion

    #region Public Methods

        public List<DomainEvent> GetDomainEvents()
        {
            return domainEvents;
        }

    #endregion

    #region Protected Methods

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

    #endregion
    }
}