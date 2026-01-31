using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class LeverButton : MonoBehaviour
{
    [SerializeField] private MenuPowerController powerController;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private string isOnParam = "IsOn";
    [SerializeField] private float inputLockTime = 0.35f;

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxToOff; // bajar
    [SerializeField] private AudioClip sfxToOn;  // subir
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;

    private bool locked;

    void Awake()
    {
        // Si el script está en ClickArea, estos GetComponent no van a encontrar el Animator del Lever.
        // Por eso conviene asignarlos por Inspector.
    }

    void OnMouseDown()
    {
        if (locked) return;
        if (powerController == null) return;
        if (animator == null || animator.runtimeAnimatorController == null) return;

        StartCoroutine(DoToggle());
    }

    private IEnumerator DoToggle()
    {
        locked = true;

        var state = powerController.CurrentState;

        // Con tu flujo Boot -> PowerOff -> Emergency:
        // Boot: click apaga (IsOn = false)
        // PowerOff: click prende (IsOn = true)
        bool goingToOn = (state == MenuPowerController.PowerState.PowerOff);

        // Anim
        animator.SetBool(isOnParam, goingToOn);

        // SFX
        if (sfxSource != null)
        {
            var clip = goingToOn ? sfxToOn : sfxToOff;
            if (clip != null) sfxSource.PlayOneShot(clip, sfxVolume);
        }

        // Cambio de estado (mitad de anim)
        yield return new WaitForSecondsRealtime(inputLockTime * 0.5f);
        powerController.ToggleFromLever();
        yield return new WaitForSecondsRealtime(inputLockTime * 0.5f);

        locked = false;
    }
}
