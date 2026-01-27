using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance { get; private set; }

    [SerializeField] private Image flashImage;

    private Coroutine running;

    private void Awake()
    {
        Instance = this;

        if (flashImage == null)
            flashImage = GetComponentInChildren<Image>();

        SetAlpha(0f);
    }

    public void Flash(Color color, float peakAlpha = 0.35f, float inTime = 0.03f, float outTime = 0.12f)
    {
        if (flashImage == null) return;

        if (running != null) StopCoroutine(running);
        running = StartCoroutine(FlashRoutine(color, peakAlpha, inTime, outTime));
    }

    private IEnumerator FlashRoutine(Color color, float peakAlpha, float inTime, float outTime)
    {
        color.a = 0f;
        flashImage.color = color;

        float t = 0f;
        while (t < inTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(0f, peakAlpha, inTime <= 0f ? 1f : t / inTime);
            SetAlpha(a);
            yield return null;
        }

        t = 0f;
        while (t < outTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(peakAlpha, 0f, outTime <= 0f ? 1f : t / outTime);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(0f);
        running = null;
    }

    private void SetAlpha(float a)
    {
        Color c = flashImage.color;
        c.a = Mathf.Clamp01(a);
        flashImage.color = c;
    }
}
