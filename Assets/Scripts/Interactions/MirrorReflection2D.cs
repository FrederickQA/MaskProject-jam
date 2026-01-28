using UnityEngine;

public class MirrorReflection2D : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform mirrorCopy;

    // Punto por donde pasa el “plano” del espejo (un Transform vacío)
    [SerializeField] private Transform mirrorPlane;

    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private SpriteRenderer mirrorRenderer;

    private void Awake()
    {
        if (playerRenderer == null && player != null)
            playerRenderer = player.GetComponentInChildren<SpriteRenderer>();

        if (mirrorRenderer == null && mirrorCopy != null)
            mirrorRenderer = mirrorCopy.GetComponentInChildren<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (player == null || mirrorCopy == null || mirrorPlane == null) return;
        if (playerRenderer == null || mirrorRenderer == null) return;

        // Copiar sprite actual (y color si querés)
        mirrorRenderer.sprite = playerRenderer.sprite;
        mirrorRenderer.color = playerRenderer.color;

        // Reflejo en X respecto a mirrorPlane.position.x
        Vector3 p = player.position;
        float dx = p.x - mirrorPlane.position.x;
        Vector3 reflected = new Vector3(mirrorPlane.position.x - dx, p.y, mirrorCopy.position.z);

        mirrorCopy.position = reflected;

        // Mantener el mismo "flip" que el player pero invertido (porque espejo)
        mirrorRenderer.flipX = !playerRenderer.flipX;
        mirrorRenderer.flipY = playerRenderer.flipY;
    }
}
