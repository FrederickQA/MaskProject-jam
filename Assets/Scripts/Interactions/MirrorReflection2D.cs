using UnityEngine;

public class MirrorReflection2D : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform mirrorCopy;
    [SerializeField] private Transform mirrorPlane;

    [Header("Renderers")]
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private SpriteRenderer mirrorRenderer;

    [Header("Mirror options")]
    [SerializeField] private bool invertFlipX = true;

    void LateUpdate()
    {
        if (player == null || mirrorCopy == null || mirrorPlane == null) return;

        // --- POSICION REFLEJADA ---
        Vector2 p = player.position;
        Vector2 p0 = mirrorPlane.position;

        Vector2 n = (Vector2)mirrorPlane.up;
        n.Normalize();

        Vector2 v = p - p0;
        Vector2 reflected = p - 2f * Vector2.Dot(v, n) * n;

        mirrorCopy.position = new Vector3(reflected.x, reflected.y, mirrorCopy.position.z);

        // --- FLIP ---
        if (playerRenderer != null && mirrorRenderer != null)
        {
            bool px = playerRenderer.flipX;
            mirrorRenderer.flipX = invertFlipX ? !px : px;
        }
    }
}
