using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LegTrigger leg))
        {
            MainCharacterComponent.mainCharacterIsDead?.Invoke();
        }
    }
}
