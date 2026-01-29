using UnityEngine;

public class CylinderState : MonoBehaviour
{
    [SerializeField] private MenuPowerController powerController;

    [Header("Cylinder Layers")]
    [SerializeField] private GameObject healthyLayer;
    [SerializeField] private GameObject brokenLayerA;
    [SerializeField] private GameObject brokenLayerB;

    [Header("Other Broken Visuals")]
    [SerializeField] private GameObject[] brokenExtras;

    private void Awake()
    {
        ApplyHealthy();
    }

    private void OnEnable()
    {
        if (powerController != null)
            powerController.OnGameStart += ApplyBroken;
    }

    private void OnDisable()
    {
        if (powerController != null)
            powerController.OnGameStart -= ApplyBroken;
    }

    private void ApplyHealthy()
    {
        if (healthyLayer != null) healthyLayer.SetActive(true);
        if (brokenLayerA != null) brokenLayerA.SetActive(false);
        if (brokenLayerB != null) brokenLayerB.SetActive(false);

        if (brokenExtras != null)
            foreach (var go in brokenExtras)
                if (go != null) go.SetActive(false);
    }

    private void ApplyBroken()
    {
        if (healthyLayer != null) healthyLayer.SetActive(false);
        if (brokenLayerA != null) brokenLayerA.SetActive(true);
        if (brokenLayerB != null) brokenLayerB.SetActive(true);

        if (brokenExtras != null)
            foreach (var go in brokenExtras)
                if (go != null) go.SetActive(true);
    }
}
