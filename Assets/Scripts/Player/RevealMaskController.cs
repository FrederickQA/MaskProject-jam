using UnityEngine;

public class RevealMaskController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject revealMaskObject; // el GO del SpriteMask
    [SerializeField] private Transform revealMaskTransform;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;

        if (revealMaskObject != null && revealMaskTransform == null)
            revealMaskTransform = revealMaskObject.transform;

        if (revealMaskObject != null)
            revealMaskObject.SetActive(false);
    }

    private void Update()
    {
        if (cam == null || revealMaskObject == null || revealMaskTransform == null) return;

        bool holding = Input.GetMouseButton(1); // click derecho

        if (holding && !revealMaskObject.activeSelf)
            revealMaskObject.SetActive(true);

        if (!holding && revealMaskObject.activeSelf)
            revealMaskObject.SetActive(false);

        if (!holding) return;

        Vector3 mp = Input.mousePosition;
        mp.z = -cam.transform.position.z;
        Vector3 world = cam.ScreenToWorldPoint(mp);
        world.z = 0f;

        revealMaskTransform.position = world;
    }
}
