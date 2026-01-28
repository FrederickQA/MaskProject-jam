using UnityEngine;

public class RevealMaskController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform revealMask;          // el GameObject del RevealMask (SpriteMask)
    [SerializeField] private SpriteRenderer playerMirror;   // PjTrim SpriteRenderer

    [Header("Settings")]
    [SerializeField] private bool hideMirrorWhileRevealing = true;

    void Awake()
    {
        if (cam == null) cam = Camera.main;

        // Estado inicial
        if (revealMask != null) revealMask.gameObject.SetActive(false);
        if (playerMirror != null) playerMirror.enabled = true;
    }

    void Update()
    {
        if (cam == null || revealMask == null) return;

        bool rmb = Input.GetMouseButton(1);

        // Toggle de estados
        revealMask.gameObject.SetActive(rmb);

        if (playerMirror != null && hideMirrorWhileRevealing)
            playerMirror.enabled = !rmb;

        // Mover máscara al mouse mientras RMB
        if (rmb)
        {
            Vector3 mw = cam.ScreenToWorldPoint(Input.mousePosition);
            mw.z = revealMask.position.z;
            revealMask.position = mw;
        }
    }
}
