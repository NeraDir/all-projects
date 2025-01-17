using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conuse : MonoBehaviour
{
    bool trigered = false;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CarMoving>())
        {
            OnTrigger();
        }
    }
    private void OnTrigger()
    {
        _particleSystem.Play();
        if (trigered)
            return;
        trigered = true;
        GameManager.instance.conesTrigger();
    }
}
