using System.Collections.Generic;
using Zenject;

namespace Nightmare
{
    public class Player
    {
    #region Public Variables

        public bool IsDead { get; private set; }

        public int CurrentHealth { get; private set; }

    #endregion

    #region Private Variables

        private readonly int               startingHealth;
        private readonly List<DomainEvent> domainEvents = new List<DomainEvent>();

        private readonly SignalBus signalBus;

    #endregion

    #region Constructor

        public Player(int startingHealth , SignalBus signalBus = null)
        {
            this.startingHealth = startingHealth;
            this.signalBus      = signalBus;
            this.signalBus?.Fire<PlayerCreated>();
            domainEvents.Add(new PlayerCreated());
            Initialize();
        }

    #endregion

    #region Public Methods

        public List<DomainEvent> GetDomainEvents()
        {
            return domainEvents;
        }

        public void MakeDie()
        {
            IsDead = true;
            signalBus?.Fire<PlayerDead>();
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;

            var playerTookDamage = new PlayerTookDamage(amount , CurrentHealth , startingHealth);
            signalBus?.Fire(playerTookDamage);
            if (CurrentHealth <= 0) MakeDie();
        }

    #endregion

    #region Private Methods

        private void Initialize()
        {
            CurrentHealth = startingHealth;
        }

    #endregion
    }

    public class PlayerTookDamage
    {
    #region Public Variables

        public int Amount         { get; }
        public int CurrentHealth  { get; }
        public int StartingHealth { get; }

    #endregion

    #region Constructor

        public PlayerTookDamage(int amount , int currentHealth , int startingHealth)
        {
            Amount         = amount;
            CurrentHealth  = currentHealth;
            StartingHealth = startingHealth;
        }

    #endregion
    }

    public class PlayerDead : DomainEvent { }

    public class PlayerCreated : DomainEvent { }
}