using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StationInteractor : MonoBehaviour
{
    [SerializeField] private UnityEvent _functionInteract;
    public void Interact()
    {
        _functionInteract?.Invoke();

    }
}
