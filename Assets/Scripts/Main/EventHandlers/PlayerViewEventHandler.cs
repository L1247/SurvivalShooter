using UnityEngine;
using Zenject;

namespace Nightmare.EventHandlers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlayerViewEventHandler
    {
    #region Private Variables

        [Inject]
        private PlayerPresenter playerPresenter;

    #endregion

    #region Constructor

        public PlayerViewEventHandler(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerDead>(OnPlayerDead);
            signalBus.Subscribe<PlayerTookDamage>(OnPlayerTookDamage);
        }

    #endregion

    #region Private Methods

        private void OnPlayerDead(PlayerDead playerDead) { }

        private void OnPlayerTookDamage(PlayerTookDamage playerTookDamage)
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