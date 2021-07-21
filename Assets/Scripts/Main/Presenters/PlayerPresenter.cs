using Nightmare;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
#region Private Variables

    [Inject]
    private DiContainer container;

    private Player player;

    [SerializeField]
    private Button buttonTakeDamage;

    [SerializeField]
    private Image healthFillImage;

    [SerializeField]
    private int startingHealth;

#endregion

#region Unity events

    // Start is called before the first frame update
    private void Start()
    {
        player = container.Instantiate<Player>(new object[] { startingHealth });
        buttonTakeDamage.onClick.AddListener(OnButtonTakeDamageClicked);
    }

#endregion

#region Public Methods

    public void PlayerTookDamage(int amount , int currentHealth , int startingHealth)
    {
        healthFillImage.fillAmount = currentHealth / (float)startingHealth;
    }

#endregion

#region Private Methods

    private void OnButtonTakeDamageClicked()
    {
        var randomDamage = Random.Range(1 , 10);
        player.TakeDamage(randomDamage);
    }

#endregion
}