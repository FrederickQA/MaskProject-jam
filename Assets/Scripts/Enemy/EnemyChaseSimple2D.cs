using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaseSimple2D : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectRadius = 4f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite deadSprite;

    [Header("Flash")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.08f;

    private Rigidbody2D rb;
    private bool dead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        if (spriteRenderer == null) // Obtener SpriteRenderer si no está asignado
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player"); // Buscar el jugador por tag
            if (p != null) player = p.transform;
        }
    }

    private void FixedUpdate()
    {
        if (dead) return;
        if (player == null) return;

        Vector2 toPlayer = (Vector2)player.position - rb.position; 
        float dist = toPlayer.magnitude; 

        if (dist > detectRadius) // Fuera de rango
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (dist > 0.001f) // Evitar división por cero
        {
            Vector2 dir = toPlayer / dist;
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead) return;

        if (collision.collider.CompareTag("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
    dead = true;

    StartCoroutine(DieRoutine());
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    private System.Collections.IEnumerator DieRoutine()
    {
    // detener movimiento
    rb.linearVelocity = Vector2.zero;

    // flash rojo
    if (spriteRenderer != null)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    // cámara
    if (CameraShake2D.Instance != null)
        CameraShake2D.Instance.Shake(0.15f, 0.15f);

    // pantalla roja
    if (ScreenFlash.Instance != null)
    ScreenFlash.Instance.Flash(Color.red, 0.35f, 0.03f, 0.12f);

    // muerte definitiva
    rb.simulated = false;

    Collider2D col = GetComponent<Collider2D>();
    if (col != null) col.enabled = false;

    if (spriteRenderer != null && deadSprite != null)
        spriteRenderer.sprite = deadSprite;
    }
}