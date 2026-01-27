using UnityEngine;

public class EnemyClickKiller2D : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float clickRadius = 0.12f;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (enemyMask.value == 0) enemyMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (cam == null) return;

        Vector3 mp = Input.mousePosition;
        mp.z = -cam.transform.position.z;
        Vector3 mw3 = cam.ScreenToWorldPoint(mp);
        Vector2 mw = new Vector2(mw3.x, mw3.y);

        Collider2D hit = clickRadius <= 0f
            ? Physics2D.OverlapPoint(mw, enemyMask)
            : Physics2D.OverlapCircle(mw, clickRadius, enemyMask);

        if (hit == null) return;

        EnemyChaseSimple2D enemy = hit.GetComponentInParent<EnemyChaseSimple2D>();
        if (enemy != null) enemy.Kill();
    }
}
