using UnityEngine;

public class FlipSprite2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D targetRb;          // el Rigidbody del player
    [SerializeField] private SpriteRenderer spriteRenderer; // el sprite que ves
    [SerializeField] private bool facesRightByDefault = true;
    [SerializeField] private float deadzone = 0.05f;
    [SerializeField] private bool useScaleFlip = false;     // activalo si flipX no se nota

    private float baseScaleX;

    private void Awake()
    {
        if (targetRb == null)
            targetRb = GetComponentInParent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            baseScaleX = Mathf.Abs(spriteRenderer.transform.localScale.x);
    }

    private void LateUpdate()
    {
        if (targetRb == null || spriteRenderer == null) return;

        float vx = targetRb.linearVelocity.x;
        if (Mathf.Abs(vx) < deadzone) return;

        bool movingLeft = vx < 0f;

        if (!useScaleFlip)
        {
            spriteRenderer.flipX = facesRightByDefault ? movingLeft : !movingLeft;
        }
        else
        {
            // Flip garantizado por escala
            float sign = (facesRightByDefault ? (movingLeft ? -1f : 1f) : (movingLeft ? 1f : -1f));
            Vector3 s = spriteRenderer.transform.localScale;
            s.x = baseScaleX * sign;
            spriteRenderer.transform.localScale = s;
        }
    }
}
