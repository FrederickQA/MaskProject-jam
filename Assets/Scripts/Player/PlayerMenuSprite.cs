using UnityEngine;

public class PlayerMenuSprite : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Collider2D playerCollider;

    [Header("Mirror")]
    [SerializeField] private GameObject mirrorRoot; // PlayerMirror (PADRE)

    [Header("Sprites")]
    [SerializeField] private Sprite menuSprite;   // Feto
    [SerializeField] private Sprite gameSprite;   // Player normal

    [Header("Scale")]
    [SerializeField] private Vector3 menuScale = new Vector3(1.6f, 1.6f, 1f);
    [SerializeField] private Vector3 gameScale = Vector3.one;

    private bool lastMenuState;

    private void Awake()
    {
        if (playerRenderer == null)
            playerRenderer = GetComponent<SpriteRenderer>();

        if (playerCollider == null)
            playerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (menuCanvas == null) return;

        lastMenuState = menuCanvas.enabled;
        ApplyState(lastMenuState);
    }

    private void Update()
    {
        if (menuCanvas == null) return;

        bool menuActive = menuCanvas.enabled;
        if (menuActive == lastMenuState) return;

        ApplyState(menuActive);
        lastMenuState = menuActive;
    }

    private void ApplyState(bool menuActive)
    {
        // Sprite del jugador
        if (playerRenderer != null)
            playerRenderer.sprite = menuActive ? menuSprite : gameSprite;

        // Escala
        transform.localScale = menuActive ? menuScale : gameScale;

        // Collider
        if (playerCollider != null)
            playerCollider.enabled = !menuActive;

        // ESPEJO COMPLETO
        if (mirrorRoot != null)
            mirrorRoot.SetActive(!menuActive);
    }
}
