using UnityEngine;
using System;

public class MenuPowerController : MonoBehaviour
{
    public enum PowerState { Boot, PowerOff, Emergency }

    [Header("State")]
    [SerializeField] private PowerState startState = PowerState.Boot;
    public PowerState CurrentState { get; private set; }

    [Header("Refs")]
    [SerializeField] private GameObject menuCanvas;        // tu MenuCanvas
    [SerializeField] private GameObject darkOverlay;       // DarkOverlay GO
    [SerializeField] private GameObject whiteLightRoot;    // WhiteLightRoot
    [SerializeField] private GameObject emergencyLightRoot;// EmergencyLightRoot
    [SerializeField] private AudioSource generatorLoop;    // audio del generador
    [SerializeField] private GameObject leverRoot;              // para desactivar el lever una vez arrancó

    // Llamado cuando pasás a Emergency por primera vez
    public event Action OnGameStart;

    private bool gameStarted;

    void Awake()
    {
        SetState(startState, applyStart: true);
    }

    public void ToggleFromLever()
    {
        // Boot -> PowerOff -> Emergency (y ahí arranca el juego)
        if (CurrentState == PowerState.Boot) SetState(PowerState.PowerOff);
        else if (CurrentState == PowerState.PowerOff) SetState(PowerState.Emergency);
        else if (CurrentState == PowerState.Emergency)
        {
            // opcional: si querés permitir togglear después del start
            // SetState(PowerState.PowerOff);
        }
    }

    private void SetState(PowerState newState, bool applyStart = false)
    {
        CurrentState = newState;

        // Boot: luz blanca + gen + menu
        if (newState == PowerState.Boot)
        {
            if (menuCanvas) menuCanvas.SetActive(true);
            if (darkOverlay) darkOverlay.SetActive(false);

            if (whiteLightRoot) whiteLightRoot.SetActive(true);
            if (emergencyLightRoot) emergencyLightRoot.SetActive(false);

            if (generatorLoop)
            {
                generatorLoop.loop = true;
                if (!generatorLoop.isPlaying) generatorLoop.Play();
            }
        }
        // PowerOff: todo apagado + oscuridad + menu sigue
        else if (newState == PowerState.PowerOff)
        {
            if (menuCanvas) menuCanvas.SetActive(true);
            if (darkOverlay) darkOverlay.SetActive(true);

            if (whiteLightRoot) whiteLightRoot.SetActive(false);
            if (emergencyLightRoot) emergencyLightRoot.SetActive(false);

            if (generatorLoop && generatorLoop.isPlaying) generatorLoop.Stop();
        }
        // Emergency: luces rojas + sin menu + arrancar juego una sola vez
        else if (newState == PowerState.Emergency)
        {
            if (darkOverlay) darkOverlay.SetActive(false);
            if (leverRoot) leverRoot.SetActive(false);
            if (whiteLightRoot) whiteLightRoot.SetActive(false);
            if (emergencyLightRoot) emergencyLightRoot.SetActive(true);

            if (generatorLoop && generatorLoop.isPlaying) generatorLoop.Stop();

            if (menuCanvas) menuCanvas.SetActive(false);

            if (!gameStarted)
            {
                gameStarted = true;
                OnGameStart?.Invoke();
            }
        }
    }
}
