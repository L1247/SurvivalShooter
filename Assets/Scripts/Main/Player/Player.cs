namespace Nightmare
{
    public class Player
    {
    #region Public Variables

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