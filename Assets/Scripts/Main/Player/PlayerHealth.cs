using UnityEngine;
using UnityEngine.UI;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour
    {
    #region Public Variables

        public AudioClip deathClip;
        public bool      godMode;
        public Color     flashColour = new Color(1f , 0f , 0f , 0.1f);
        public float     flashSpeed  = 5f;
        public Image     damageImage;
        public int       currentHealth;
        public int       startingHealth = 100;

    #endregion

    #region Private Variables

        private Animator       anim;
        private AudioSource    playerAudio;
        private bool           damaged;
        private bool           isDead;
        private PlayerMovement playerMovement;
        private PlayerShooting playerShooting;

        [SerializeField]
        private Image healthFillImage;

    #endregion

    #region Public Methods

        public void ResetPlayer()
        {
            // currentHealth          = startingHealth;
            playerMovement.enabled = true;
            playerShooting.enabled = true;

            anim.SetBool("IsDead" , false);
        }

        public void RestartLevel()
        {
            EventManager.TriggerEvent("GameOver");
        }


        public void TakeDamage(int amount)
        {
            if (godMode)
                return;

            // Set the damaged flag so the screen will flash.
            damaged = true;

            // #region Domain
            //
            //     // Reduce the current health by the damage amount.
            //     currentHealth -= amount;
            //
            // #endregion

            // Set the health bar's value to the current health.
            // 100f / 100f = 1f
            // 10f / 100f = 0.1f
            healthFillImage.fillAmount = currentHealth / (float)startingHealth;

            // Play the hurt sound effect.
            playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...

        #region Domain

            // if (currentHealth <= 0 && !isDead)
            // ... it should die.
            // Death();

        #endregion
        }

    #endregion

    #region Private Methods

        private void Awake()
        {
            // Setting up the references.
            anim           = GetComponent<Animator>();
            playerAudio    = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            playerShooting = GetComponentInChildren<PlayerShooting>();

            ResetPlayer();
        }

        private void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            playerShooting.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetBool("IsDead" , true);

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }


        private void Update()
        {
            // If the player has just been damaged...
            if (damaged)
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            // Otherwise...
            else
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp(damageImage.color , Color.clear , flashSpeed * Time.deltaTime);

            // Reset the damaged flag.
            damaged = false;
        }

    #endregion
    }
}