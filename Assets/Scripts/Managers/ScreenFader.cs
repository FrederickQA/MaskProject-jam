using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image blackImage;

    public void SetAlpha(float a)
    {
        if (blackImage == null) return;
        Color c = blackImage.color;
        c.a = Mathf.Clamp01(a);
        blackImage.color = c;
    }

    public IEnumerator FadeTo(float targetAlpha, float duration)
    {
        if (blackImage == null) yield break;

        float startAlpha = blackImage.color.a;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, duration <= 0f ? 1f : t / duration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }
}
