namespace Nightmare
{
    public class Player : AggregateRoot
    {
    #region Public Variables

        public bool IsDead { get; private set; }

        public int CurrentHealth { get; private set; }

    #endregion

    #region Private Variables

        private readonly int startingHealth;

    #endregion

    #region Constructor

        public Player(int startingHealth , IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            this.startingHealth = startingHealth;
            AddDomainEvent(new PlayerCreated());
            domainEventBus.PostAll(this);
            Initialize();
        }

    #endregion

    #region Public Methods

        public void MakeDie()
        {
            IsDead = true;
            AddDomainEvent(new PlayerDead());
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            var playerTookDamage = new PlayerTookDamage(amount , CurrentHealth , startingHealth);
            AddDomainEvent(playerTookDamage);
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

    public class PlayerTookDamage : DomainEvent
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