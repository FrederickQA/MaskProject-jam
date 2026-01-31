using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Light2DIntensityPulse : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private float baseIntensity = 1.0f;
    [SerializeField] private float pulseAmplitude = 0.25f;
    [SerializeField] private float pulseSpeed = 0.8f;

    [Header("Flicker (optional)")]
    [SerializeField] private bool useFlicker = true;
    [SerializeField] private float flickerAmplitude = 0.08f;
    [SerializeField] private float flickerSpeed = 9.0f;

    [Header("Time")]
    [SerializeField] private bool useUnscaledTime = false;

    private Light2D light2D;
    private float seed;

    void Awake()
    {
        light2D = GetComponent<Light2D>();
        seed = Random.value * 1000f;
    }

    void OnEnable()
    {
        if (light2D != null)
            light2D.intensity = baseIntensity;
    }

    void Update()
    {
        if (light2D == null) return;

        float t = useUnscaledTime ? Time.unscaledTime : Time.time;

        float pulse = Mathf.Sin((t + seed) * pulseSpeed) * pulseAmplitude;

        float flicker = 0f;
        if (useFlicker)
        {
            float n = Mathf.PerlinNoise(seed, (t * flickerSpeed));
            flicker = (n - 0.5f) * 2f * flickerAmplitude;
        }

        float value = baseIntensity + pulse + flicker;
        if (value < 0f) value = 0f;

        light2D.intensity = value;
    }
}
