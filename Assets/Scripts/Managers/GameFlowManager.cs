using System.Collections;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private MonoBehaviour playerMovementScript; // tu PlayerMovement2D (o el script que uses)
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private ScreenFader screenFader;

    [Header("Lights")]
    [SerializeField] private GameObject normalLightsRoot;
    [SerializeField] private GameObject emergencyLightsRoot;

    [Header("Audio")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip leverDownClip;
    [SerializeField] private AudioClip leverUpClip;
    [SerializeField] private AudioClip fuseBreakClip;
    [SerializeField] private AudioClip glassBreakClip;
    [SerializeField] private AudioClip emergencyOnClip;

    private bool inMenu = true;
    private int leverStage = 0;
    private bool sequenceRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        EnterMenuMode();
    }

    public void EnterMenuMode()
    {
        inMenu = true;
        leverStage = 0;
        sequenceRunning = false;

        if (menuCanvas) menuCanvas.enabled = true;

        SetPlayerControl(false);

        if (normalLightsRoot) normalLightsRoot.SetActive(true);
        if (emergencyLightsRoot) emergencyLightsRoot.SetActive(false);

        if (screenFader) screenFader.SetAlpha(0f);
    }

    private void EnterGameplayMode()
    {
        inMenu = false;

        if (menuCanvas) menuCanvas.enabled = false;

        SetPlayerControl(true);
    }

    private void SetPlayerControl(bool enabled)
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = enabled;
    }

    public void OnLeverPulled(bool leverDown)
    {
        if (!inMenu) return;
        if (sequenceRunning) return;

        if (leverStage == 0)
        {
            StartCoroutine(MenuPlayStage1(leverDown));
        }
        else if (leverStage == 1)
        {
            StartCoroutine(MenuPlayStage2(leverDown));
        }
    }

    private IEnumerator MenuPlayStage1(bool leverDown)
    {
        sequenceRunning = true;

        PlaySfx(leverDown ? leverDownClip : leverUpClip);

        // Palanca baja + se oscurece todo
        if (screenFader) yield return screenFader.FadeTo(1f, 0.25f);

        leverStage = 1;
        sequenceRunning = false;
    }

    private IEnumerator MenuPlayStage2(bool leverDown)
    {
        sequenceRunning = true;

        PlaySfx(leverDown ? leverDownClip : leverUpClip);

        // vuelve la luz
        if (screenFader) yield return screenFader.FadeTo(0f, 0.12f);

        // se rompe fusible y se apaga de nuevo
        yield return new WaitForSeconds(0.08f);
        PlaySfx(fuseBreakClip);

        if (screenFader) yield return screenFader.FadeTo(1f, 0.08f);

        // vidrio
        yield return new WaitForSeconds(0.12f);
        PlaySfx(glassBreakClip);

        // luces de emergencia
        if (normalLightsRoot) normalLightsRoot.SetActive(false);
        if (emergencyLightsRoot) emergencyLightsRoot.SetActive(true);
        PlaySfx(emergencyOnClip);

        // quitar el negro, pero mantener mood por emergencias
        if (screenFader) yield return screenFader.FadeTo(0.15f, 0.25f);

        EnterGameplayMode();

        sequenceRunning = false;
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxSource == null) return;
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
