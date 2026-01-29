using UnityEngine;

public class GameStartFixes : MonoBehaviour
{
    [SerializeField] private MenuPowerController power;

    [Header("Enable on game start")]
    [SerializeField] private Behaviour[] enableThese; // arrastrá PlayerController, Enemy scripts, etc

    private void OnEnable()
    {
        if (power != null) power.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        if (power != null) power.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        // Por si el menú había pausado el juego
        Time.timeScale = 1f;

        // Rehabilitar scripts clave (por ejemplo tu PlayerController)
        if (enableThese != null)
        {
            for (int i = 0; i < enableThese.Length; i++)
            {
                if (enableThese[i] != null)
                    enableThese[i].enabled = true;
            }
        }
    }
}
