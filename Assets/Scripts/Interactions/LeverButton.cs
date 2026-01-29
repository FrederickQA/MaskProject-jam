using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class LeverButton : MonoBehaviour
{
    [SerializeField] private MenuPowerController powerController;
    [SerializeField] private Animator animator;

    [Header("Animator Param")]
    [SerializeField] private string isOnParam = "IsOn";

    [Header("Lock")]
    [SerializeField] private float inputLockTime = 0.35f;

    private bool locked;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (locked) return;
        if (powerController == null) return;
        if (animator == null) return;

        StartCoroutine(DoToggle());
    }

    private IEnumerator DoToggle()
    {
        locked = true;

        var state = powerController.CurrentState;

        // Boot -> PowerOff: IsOn = false (baja)
        // PowerOff -> Emergency: IsOn = true (sube)
        if (state == MenuPowerController.PowerState.Boot)
            animator.SetBool(isOnParam, false);
        else if (state == MenuPowerController.PowerState.PowerOff)
            animator.SetBool(isOnParam, true);

        // Cambiá luces/estado a mitad de animación
        yield return new WaitForSecondsRealtime(inputLockTime * 0.5f);

        powerController.ToggleFromLever();

        yield return new WaitForSecondsRealtime(inputLockTime * 0.5f);

        locked = false;
    }
}
