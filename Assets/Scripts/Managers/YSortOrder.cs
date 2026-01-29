using UnityEngine;
using UnityEngine.Rendering;

[DisallowMultipleComponent]
public class YSortOrder : MonoBehaviour
{
    [Header("Order = -(Y * pixelsPerUnit) + offset")]
    [SerializeField] private int offset = 0;
    [SerializeField] private float precision = 100f; // 100 = suficientemente fino

    [Header("Auto")]
    [SerializeField] private bool useSortingGroupIfPresent = true;

    private SpriteRenderer sr;
    private SortingGroup sg;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sg = GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        int order = -(int)(transform.position.y * precision) + offset;

        if (useSortingGroupIfPresent && sg != null)
        {
            sg.sortingOrder = order;
            return;
        }

        if (sr != null)
            sr.sortingOrder = order;
    }
}
