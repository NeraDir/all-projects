using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapcomponent : MonoBehaviour
{
    [SerializeField]
    private Quaternion _startRotation;

    private void Start()
    {
        transform.rotation = _startRotation;
    }
}
