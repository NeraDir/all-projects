using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bullcomponent : MonoBehaviour
{

    public static UnityEvent roadReached = new UnityEvent();
    public static UnityEvent bullisDeath = new UnityEvent();

    private bool _fastRun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out starcomponent star))
        {

        }
        if (other.TryGetComponent(out platformcomponent platform))
        {
            roadReached?.Invoke();
        }
        if (other.TryGetComponent(out spikecomponent spike))
        {
            bullisDeath?.Invoke();
        }
        if (other.TryGetComponent(out woodtrapcomponent woodTrap) && !_fastRun)
        {
            bullisDeath?.Invoke();
        }
    }
}
