using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrapComponent : MonoBehaviour
{
    [SerializeField] private GameObject _destroyEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TigerTrigger tigerTrigger))
        {
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            HealthManager.changeHealth?.Invoke(-1);
            Destroy(gameObject);
        }
    }
}
