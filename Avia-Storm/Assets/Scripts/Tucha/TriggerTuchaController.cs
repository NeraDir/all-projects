using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuchaController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.SetInteger("State", 1);
        StartCoroutine(AnimTimer(0.6f));
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetInteger("State", 2);
        Movement.InFly = false;
        StopAllCoroutines();
        StartCoroutine(AnimTimer(0.6f));
    }

    IEnumerator AnimTimer(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetInteger("State", 0);
    }
}
