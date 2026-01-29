using UnityEngine;

public class GameStartController : MonoBehaviour
{
    [SerializeField] private MenuPowerController power;
    [SerializeField] private GameObject playerRoot; // Player GO
    [SerializeField] private Collider2D playerCollider; // opcional

    void Awake()
    {
        if (power != null)
            power.OnGameStart += StartGame;
    }

    void OnDestroy()
    {
        if (power != null)
            power.OnGameStart -= StartGame;
    }

    private void StartGame()
    {
        if (playerRoot) playerRoot.SetActive(true);
        if (playerCollider) playerCollider.enabled = true;


    }
}
