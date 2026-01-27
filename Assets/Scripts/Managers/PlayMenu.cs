using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private MonoBehaviour playerMovementScript;

    private void Start()
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;
    }

    public void Play()
    {
        if (menuCanvas != null)
            menuCanvas.enabled = false;

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }
}
