using UnityEngine;

public class GlassBreakOnGameStart : MonoBehaviour
{
    [SerializeField] private MenuPowerController power;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip glassBreak;
    [SerializeField] private float volume = 1f;

    private void Awake()
    {
        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (power != null)
            power.OnGameStart += HandleGameStart;
    }

    private void OnDisable()
    {
        if (power != null)
            power.OnGameStart -= HandleGameStart;
    }

    private void HandleGameStart()
    {
        if (sfxSource == null || glassBreak == null) return;
        sfxSource.PlayOneShot(glassBreak, volume);
    }
}
