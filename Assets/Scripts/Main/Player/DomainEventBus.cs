using System;
using System.Collections.Generic;
using Nightmare;
using Zenject;

namespace rStar
{
    public class DomainEventBus : IDomainEventBus
    {
    #region Public Variables

        public Dictionary<Type , List<Action<object>>> CallBacks { get; }

    #endregion

    #region Private Variables

        private readonly SignalBus signalBus;

    #endregion

    #region Constructor

        [Inject]
        public DomainEventBus(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            CallBacks      = new Dictionary<Type , List<Action<object>>>();
            this.signalBus.Subscribe<DomainEvent>(Publish);
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

        public void Register<T>(Action<T> callBackAction , bool isEarly = false)
        {
            var type        = typeof(T);
            var containsKey = CallBacks.ContainsKey(type);
            if (containsKey)
            {
                var actions = CallBacks[type];
                if (isEarly) actions.Insert(0 , o => callBackAction((T)o));
                else actions.Add(o => callBackAction((T)o));
            }
            else
            {
                var actions = new List<Action<object>>();
                actions.Add(o => callBackAction((T)o));
                CallBacks.Add(type , actions);
            }
        }

    #endregion

    #region Private Methods

        private void Post(DomainEvent domainEvent)
        {
            signalBus.TryFire(domainEvent);
        }

        private void Publish(DomainEvent domainEvent)
        {
            var type        = domainEvent.GetType();
            var containsKey = CallBacks.ContainsKey(type);
            if (containsKey)
            {
                var actions = CallBacks[type];
                actions.ForEach(action => action.Invoke(domainEvent));
            }
        }

    #endregion
    }
}