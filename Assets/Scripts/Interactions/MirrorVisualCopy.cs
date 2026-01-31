using UnityEngine;

public class MirrorVisualCopy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private SpriteRenderer mirrorRenderer;
    [SerializeField] private bool invertFlipX = true;

    void Awake()
    {
        if (mirrorRenderer == null) mirrorRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (playerRenderer == null || mirrorRenderer == null) return;

        mirrorRenderer.sprite = playerRenderer.sprite;

        bool px = playerRenderer.flipX;
        mirrorRenderer.flipX = invertFlipX ? !px : px;
    }
}
