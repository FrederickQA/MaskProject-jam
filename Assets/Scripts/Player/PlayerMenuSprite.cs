using UnityEngine;

public class PlayerMenuSprite : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private MenuPowerController powerController; // en vez de Canvas
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Collider2D playerCollider;

    [Header("Movement")]
    [SerializeField] private MonoBehaviour playerMovement; // arrastrá PlayerMovement2D acá

    [Header("Mirror")]
    [SerializeField] private GameObject mirrorRoot;

    [Header("Sprites")]
    [SerializeField] private Sprite menuSprite;
    [SerializeField] private Sprite gameSprite;

    [Header("Scale")]
    [SerializeField] private Vector3 menuScale = new Vector3(1.6f, 1.6f, 1f);
    [SerializeField] private Vector3 gameScale = Vector3.one;

    private void Awake()
    {
        if (playerRenderer == null) playerRenderer = GetComponent<SpriteRenderer>();
        if (playerCollider == null) playerCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        if (powerController != null) powerController.OnGameStart += HandleGameStart;
    }

    private void OnDisable()
    {
        if (powerController != null) powerController.OnGameStart -= HandleGameStart;
    }

    private void Start()
    {
        // Estado inicial: estamos en menú
        ApplyMenuState(true);
    }

    private void HandleGameStart()
    {
        ApplyMenuState(false);
    }

    private void ApplyMenuState(bool menuActive)
    {
        if (playerRenderer != null)
            playerRenderer.sprite = menuActive ? menuSprite : gameSprite;

        transform.localScale = menuActive ? menuScale : gameScale;

        if (playerCollider != null)
            playerCollider.enabled = !menuActive;

        if (playerMovement != null)
            playerMovement.enabled = !menuActive;

        if (mirrorRoot != null)
            mirrorRoot.SetActive(!menuActive);
    }
}
