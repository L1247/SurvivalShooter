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
            domainEventBus.Register<PlayerDead>(OnPlayerDead);
            domainEventBus.Register<PlayerTookDamage>(OnPlayerTookDamage);
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
    }
}