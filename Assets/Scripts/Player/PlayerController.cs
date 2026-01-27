using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;
    
    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool spriteFacesRightByDefault = true;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

    if (spriteRenderer == null)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        input = Vector2.ClampMagnitude(input, 1f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
    private void UpdateFlip()
    {
        if (spriteRenderer == null) return;

        // Solo flip si hay intención horizontal
        if (Mathf.Abs(input.x) < 0.01f) return;

        bool movingLeft = input.x < 0f;

        // flipX true lo hace mirar a la izquierda
        // invertimos con spriteFacesRightByDefault = false
        spriteRenderer.flipX = spriteFacesRightByDefault ? movingLeft : !movingLeft;
    }
}
