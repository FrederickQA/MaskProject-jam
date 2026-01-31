using UnityEngine;

public class MirrorInputMover : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player; // solo para anclar el punto base si querés

    [Header("Tuning")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 worldOffset; // lo ajustás a mano hasta que calce

    [Header("Invert")]
    [SerializeField] private bool invertX = true;
    [SerializeField] private bool invertY = false;

    void Start()
    {
        // Arranca alineado cerca del player, pero con offset
        if (player != null)
            transform.position = player.position + worldOffset;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (invertX) x = -x;
        if (invertY) y = -y;

        Vector3 delta = new Vector3(x, y, 0f).normalized * (moveSpeed * Time.deltaTime);

        transform.position += delta;

        // Si querés que nunca se “vaya” del player, podés re-anclar suavemente:
        // if (player != null) transform.position = player.position + worldOffset + (transform.position - (player.position + worldOffset));
    }
}
