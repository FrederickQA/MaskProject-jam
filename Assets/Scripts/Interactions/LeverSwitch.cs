using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool startOn = true; // opcional para setear estado inicial

    private bool isOn;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();

        isOn = startOn;
        animator.SetBool("IsOn", isOn);
    }

    void OnMouseDown()
    {
        isOn = !isOn;
        animator.SetBool("IsOn", isOn);
    }
}
