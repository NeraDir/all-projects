using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBonusBallComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bonusTrigger1"))
        {
            Do();
            StarsBallsBonusGameControllerComponent.xesBallsLister[0]++;
        }
        else if (other.CompareTag("bonusTrigger2"))
        {
            StarsBallsBonusGameControllerComponent.xesBallsLister[1]++;
            Do();
        }
        else if (other.CompareTag("bonusTrigger3"))
        {
            StarsBallsBonusGameControllerComponent.xesBallsLister[2]++;
            Do();
        }
        else if (other.CompareTag("bonusTrigger4"))
        {
            StarsBallsBonusGameControllerComponent.xesBallsLister[3]++;
            Do();
        }
    }

    private void Do() 
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(()=>Destroy(gameObject));
    }
}
