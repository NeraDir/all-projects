using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void GoAnim()
    {
        anim.SetFloat("speed", 1f);
    }

    public void StopAnim()
    {
        anim.SetFloat("speed", 0);
    }
}
