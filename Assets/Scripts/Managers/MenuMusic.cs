using System.Collections;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeOutTime = 1.0f;

    private bool wasMenuActive;
    private float initialVolume;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        initialVolume = audioSource.volume;
        wasMenuActive = false;
    }

    private void Update()
    {
        if (menuCanvas == null || audioSource == null) return;

        bool isMenuActive = menuCanvas.enabled;

        // Menú acaba de activarse
        if (isMenuActive && !wasMenuActive)
        {
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);

            audioSource.volume = initialVolume;
            audioSource.Play();
        }

        // Menú acaba de desactivarse
        if (!isMenuActive && wasMenuActive)
        {
            if (fadeRoutine != null) StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(FadeOut());
        }

        wasMenuActive = isMenuActive;
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutTime)
        {
            t += Time.unscaledDeltaTime; // no depende del timeScale
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = initialVolume;
        fadeRoutine = null;
    }
}
