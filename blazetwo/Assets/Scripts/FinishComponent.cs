using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MainCharacterComponent character))
        {
            MainCharacterComponent.mainCharacterIsFinished?.Invoke();
        }
    }
}
