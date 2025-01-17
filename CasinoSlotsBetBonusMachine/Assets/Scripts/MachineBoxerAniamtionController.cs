using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBoxerAniamtionController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAniamtion(int index) 
    {
        animator.SetInteger("BoxerState", index);
    }
}
