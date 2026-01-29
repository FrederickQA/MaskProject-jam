using UnityEngine;

public class CylinderState : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    [Header("Cylinder Layers")]
    [SerializeField] private GameObject healthyLayer;
    [SerializeField] private GameObject brokenLayerA;
    [SerializeField] private GameObject brokenLayerB;

    [Header("Other Broken Visuals (menu OFF)")]
    [SerializeField] private GameObject[] brokenExtras;

    private bool wasMenuActive;

    private void Start()
    {
        if (menuCanvas == null) return;
        wasMenuActive = menuCanvas.enabled;
        ApplyState(wasMenuActive);
    }

    private void Update()
    {
        if (menuCanvas == null) return;

        bool isMenuActive = menuCanvas.enabled;
        if (isMenuActive == wasMenuActive) return;

        ApplyState(isMenuActive);
        wasMenuActive = isMenuActive;
    }

    private void ApplyState(bool menuActive)
    {
        if (healthyLayer != null) healthyLayer.SetActive(menuActive);

        bool broken = !menuActive;
        if (brokenLayerA != null) brokenLayerA.SetActive(broken);
        if (brokenLayerB != null) brokenLayerB.SetActive(broken);

        if (brokenExtras != null)
        {
            for (int i = 0; i < brokenExtras.Length; i++)
            {
                if (brokenExtras[i] != null)
                    brokenExtras[i].SetActive(broken);
            }
        }
    }
}
