using UnityEngine;
using Zenject;

namespace Nightmare.EventHandlers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlayerViewEventHandler
    {
    #region Private Variables

        [Inject(Optional = true)]
        private PlayerPresenter playerPresenter;

    #endregion

    #region Constructor

        public PlayerViewEventHandler(IDomainEventBus domainEventBus)
        {
            Register(domainEventBus , true);
        }

    #endregion

    #region Public Methods

        public void OnPlayerDead(PlayerDead playerDead) { }

        public void OnPlayerTookDamage(PlayerTookDamage playerTookDamage)
        {
            var amount         = playerTookDamage.Amount;
            var currentHealth  = playerTookDamage.CurrentHealth;
            var startingHealth = playerTookDamage.StartingHealth;
            Debug.Log(
                $"OnPlayerTookDamage - Amount {amount} , CurrentHealth {currentHealth} , startingHealth {startingHealth}");
            playerPresenter.PlayerTookDamage(amount , currentHealth , startingHealth);
        }

    #endregion

    #region Private Methods

        private void Register(IDomainEventBus domainEventBus , bool isEarly)
        {
            domainEventBus.Register<PlayerDead>(OnPlayerDead , isEarly);
            domainEventBus.Register<PlayerTookDamage>(OnPlayerTookDamage , isEarly);
        }

    #endregion
    }
}