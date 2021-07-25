using System;

namespace Nightmare
{
    public interface IDomainEventBus
    {
    #region Public Methods

        void PostAll(AggregateRoot aggregateRoot);

        void Register<T>(Action<T> callBackAction , bool isEarly = false);

    #endregion
    }
}