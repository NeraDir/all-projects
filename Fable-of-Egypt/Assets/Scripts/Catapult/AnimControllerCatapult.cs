using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControllerCatapult : MonoBehaviour
{
    [SerializeField] private Animator aminator;


    public void SetWin()
    {
        aminator.SetTrigger("Win");
    }

    public void SetLose()
    {
        aminator.SetTrigger("Lose");
    }
}
