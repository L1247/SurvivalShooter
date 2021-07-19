namespace Nightmare
{
    public class Player
    {
    #region Public Variables

        public bool IsDead { get; private set; }

        public int CurrentHealth { get; private set; }

    #endregion

    #region Private Variables

        private readonly int startingHealth;

    #endregion

    #region Constructor

        public Player(int startingHealth)
        {
            this.startingHealth = startingHealth;
            Initialize();
        }

    #endregion

    #region Public Methods

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            IsDead        =  CurrentHealth <= 0;
        }

    #endregion

    #region Private Methods

        private void Initialize()
        {
            CurrentHealth = startingHealth;
        }

    #endregion
    }
}