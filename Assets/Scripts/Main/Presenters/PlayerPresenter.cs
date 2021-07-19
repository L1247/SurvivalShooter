using Nightmare;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
#region Private Variables

    [SerializeField]
    private int startingHealth;

    [SerializeField]
    private PlayerHealth playerHealth;

#endregion

#region Unity events

    // Start is called before the first frame update
    private void Start()
    {
        var player = new Player(startingHealth);
    }

#endregion
}