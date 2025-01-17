using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanBullet : MonoBehaviour
{
    public bool isEnemieBullet;

    public Vector3 direction;

    private void LateUpdate()
    {
        if (isEnemieBullet)
            transform.position += direction * 0.75f * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DragonLanEnemie enemie))
        {
            if (!isEnemieBullet)
            {
                enemie.Death();
            }
        }
        if (other.TryGetComponent(out DragonLanController dragon))
        {
            if (isEnemieBullet)
            {
                DragonLanGameController.dragonAlive = false;
                Destroy(gameObject);
            }
            
        }
        if (other.TryGetComponent(out DragonLanGate gate))
        {
            gate.Outed();
        }
    }
}
