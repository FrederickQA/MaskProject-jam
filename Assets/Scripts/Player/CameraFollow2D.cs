using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;

    private Rigidbody2D targetRb;
    private Vector3 offset;

    private void Awake()
    {
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow2D: Target no asignado");
            enabled = false;
            return;
        }

        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos;

        if (targetRb != null)
            targetPos = (Vector3)targetRb.position;
        else
            targetPos = target.position;

        Vector3 desired = targetPos + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            smoothSpeed * Time.deltaTime
        );
    }
}
