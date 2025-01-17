using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public delegate void HeadSweetieTriggerDelegate();
    public static event HeadSweetieTriggerDelegate HeadSweetieTriggerEvent;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out HeadSweetie headSweetie))
        {
            if (HeadSweetieTriggerEvent != null)
            {
                HeadSweetieTriggerEvent();
            }
        }
    }
}
