using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _trigger;
    [SerializeField] private Vector2 _bias;

    private void Awake()
    {
        if(_trigger == null)
        this.enabled = false;
    }
    private void Update()
    {
        transform.position = new Vector3(_trigger.position.x + _bias.x, _trigger.position.y + _bias.y, transform.position.z);
    }
}
