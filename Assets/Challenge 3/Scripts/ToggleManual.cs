using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManual : MonoBehaviour
{
    private Animator animator;
    const string OPEN_ANIMATOR_BOOL = "isOpen";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        animator.SetBool(OPEN_ANIMATOR_BOOL, !animator.GetBool(OPEN_ANIMATOR_BOOL));
    }
}
