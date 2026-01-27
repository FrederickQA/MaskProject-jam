using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Collider2D clickCollider;
    [SerializeField] private float downAngle = -45f;
    [SerializeField] private float upAngle = 0f;

    private bool isDown = false;

    private void Reset()
    {
        clickCollider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        // Click en palanca
        Toggle();
    }

    private void Toggle()
    {
        isDown = !isDown;

        float z = isDown ? downAngle : upAngle;
        transform.localRotation = Quaternion.Euler(0f, 0f, z);

        if (GameFlowManager.Instance != null)
            GameFlowManager.Instance.OnLeverPulled(isDown);
    }
}
