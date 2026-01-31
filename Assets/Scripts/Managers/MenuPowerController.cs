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
    [SerializeField] private AudioSource tubeHumLoop; // zumbido de tubos

    [SerializeField] private GameObject leverRoot;              // para desactivar el lever una vez arrancó

    [Header("Emergency Delay")]
    [SerializeField] private float emergencyLightsDelay = 2f;

    // Llamado cuando pasás a Emergency por primera vez
    public event Action OnGameStart;
    public event Action<PowerState> OnStateChanged;

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
    private void SetGenerator(bool on)
    {
    if (!generatorLoop) return;

    generatorLoop.loop = true;

    if (on)
    {
        if (!generatorLoop.isPlaying)
        {
            generatorLoop.time = 0f;          // opcional: arrancar desde el inicio
            generatorLoop.volume = 0.3f;      // si querés forzar volumen
            generatorLoop.Play();
        }
    }
    else
    {
        if (generatorLoop.isPlaying)
            generatorLoop.Stop();
    }
    }
    private void SetState(PowerState newState, bool applyStart = false)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);

        // Boot: luz blanca + gen + menu
        if (newState == PowerState.Boot)
        {
            if (menuCanvas) menuCanvas.SetActive(true);
            if (darkOverlay) darkOverlay.SetActive(false);

            if (whiteLightRoot) whiteLightRoot.SetActive(true);
            if (emergencyLightRoot) emergencyLightRoot.SetActive(false);

            SetLoop(generatorLoop, true);
            SetLoop(tubeHumLoop, true);
        }
        // PowerOff: todo apagado + oscuridad + menu sigue
        else if (newState == PowerState.PowerOff)
        {
            if (menuCanvas) menuCanvas.SetActive(true);
            if (darkOverlay) darkOverlay.SetActive(true);

            if (whiteLightRoot) whiteLightRoot.SetActive(false);
            if (emergencyLightRoot) emergencyLightRoot.SetActive(false);

            SetLoop(generatorLoop, false);
            SetLoop(tubeHumLoop, false);
        }
        // Emergency: luces rojas + sin menu + arrancar juego una sola vez
        else if (newState == PowerState.Emergency)
        {
            if (darkOverlay) darkOverlay.SetActive(false);
            if (leverRoot) leverRoot.SetActive(false);
            if (whiteLightRoot) whiteLightRoot.SetActive(false);
            StartCoroutine(EnableEmergencyLightsWithDelay());

            SetLoop(generatorLoop, false);
            SetLoop(tubeHumLoop, false);

            if (menuCanvas) menuCanvas.SetActive(false);

            if (!gameStarted)
            {
                gameStarted = true;
                OnGameStart?.Invoke();
            }
        }
    }
    private System.Collections.IEnumerator EnableEmergencyLightsWithDelay()
    {
        yield return new WaitForSeconds(emergencyLightsDelay);

        if (emergencyLightRoot)
            emergencyLightRoot.SetActive(true);
    }
    private void SetLoop(AudioSource src, bool on)
    {
    if (!src) return;
    src.loop = true;

    if (on)
    {
        if (!src.isPlaying)
        {
            src.time = 0f;
            src.Play();
        }
    }
    else
    {
        if (src.isPlaying)
            src.Stop();
    }
    }
}
