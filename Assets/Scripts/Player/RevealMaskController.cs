using UnityEngine;

public class RevealMaskController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform revealMask;              // GameObject del RevealMask
    [SerializeField] private SpriteRenderer playerMirror;       // PjTrim SpriteRenderer
    [SerializeField] private Collider2D mirrorArea;             // Collider del espejo

    [Header("Settings")]
    [SerializeField] private bool hideMirrorWhileRevealing = true;
    [SerializeField] private float revealRadius = 0.3f;         // radio efectivo del reveal
    [SerializeField] private LayerMask mirrorMask;              // layer del espejo (opcional pero recomendado)

    void Awake()
    {
        if (cam == null) cam = Camera.main;

        if (revealMask != null)
            revealMask.gameObject.SetActive(false);

        if (playerMirror != null)
            playerMirror.enabled = true;
    }

    void Update()
    {
        if (cam == null || revealMask == null) return;

        bool rmb = Input.GetMouseButton(1);

        // activar/desactivar la máscara
        revealMask.gameObject.SetActive(rmb);

        // mouse en mundo
        Vector3 mw3 = cam.ScreenToWorldPoint(Input.mousePosition);
        mw3.z = revealMask.position.z;
        Vector2 mw = new Vector2(mw3.x, mw3.y);

        // mover máscara
        if (rmb)
            revealMask.position = mw3;

        // ocultar reflejo SOLO si el reveal toca el espejo
        if (playerMirror != null && hideMirrorWhileRevealing && mirrorArea != null)
        {
            bool revealOnMirror = false;

            if (rmb)
            {
                Collider2D hit = (mirrorMask.value != 0)
                    ? Physics2D.OverlapCircle(mw, revealRadius, mirrorMask)
                    : Physics2D.OverlapCircle(mw, revealRadius);

                revealOnMirror = (hit != null && (hit == mirrorArea || hit.transform.IsChildOf(mirrorArea.transform)));
            }

            playerMirror.enabled = !revealOnMirror;
        }
    }
}
