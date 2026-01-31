using UnityEngine;

public class GlassBreakOnPowerOff : MonoBehaviour
{
    [SerializeField] private MenuPowerController power;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip glassBreak;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private void Awake()
    {
        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (power != null)
            power.OnStateChanged += HandleState;
    }

    private void OnDisable()
    {
        if (power != null)
            power.OnStateChanged -= HandleState;
    }

    private void HandleState(MenuPowerController.PowerState state)
    {
        if (state != MenuPowerController.PowerState.PowerOff) return;

        if (sfxSource == null || glassBreak == null) return;
        sfxSource.PlayOneShot(glassBreak, volume);
    }
}
