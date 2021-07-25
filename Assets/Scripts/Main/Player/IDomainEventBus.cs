namespace Nightmare
{
    public interface IDomainEventBus
    {
    #region Public Methods

        void PostAll(AggregateRoot aggregateRoot);

    #endregion
    }
}