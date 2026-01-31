using UnityEngine;

public class RotateZ : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 90f;
    [SerializeField] private bool useUnscaledTime = false;

    void Update()
    {
        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        transform.Rotate(0f, 0f, -degreesPerSecond * dt);
    }
}
