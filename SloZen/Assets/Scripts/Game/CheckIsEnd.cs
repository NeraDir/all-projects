using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CheckWhatIsPacks check))
        {
            transform.parent.GetComponentInChildren<Collider>().isTrigger = true;
        }
    }
}
