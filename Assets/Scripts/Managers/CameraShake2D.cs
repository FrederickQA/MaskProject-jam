using System.Collections;
using UnityEngine;

public class CameraShake2D : MonoBehaviour
{
    public static CameraShake2D Instance { get; private set; }

    private Vector3 originalLocalPos;
    private Coroutine running;
    private bool isShaking;

    private void Awake()
    {
        Instance = this;
        originalLocalPos = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        if (isShaking) return; // 👈 clave: no acumula

        running = StartCoroutine(ShakeRoutine(duration, strength));
    }

    private IEnumerator ShakeRoutine(float duration, float strength)
    {
        isShaking = true;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Vector2 r = Random.insideUnitCircle * strength;
            transform.localPosition = originalLocalPos + new Vector3(r.x, r.y, 0f);

            yield return null;
        }

        transform.localPosition = originalLocalPos;
        isShaking = false;
        running = null;
    }
}
