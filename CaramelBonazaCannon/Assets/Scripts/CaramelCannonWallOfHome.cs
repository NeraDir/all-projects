using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaramelCannonWallOfHome : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CaramelCannonEnemieComponent enemies))
        {
            if (enemies.isBoss)
            {
                enemies.Death(1000000);
                CaramelCanonGameManager.caramelCannonHealth -= 3;
            }
            else
            {
                enemies.Death(1000000);
                CaramelCanonGameManager.caramelCannonHealth--;
            }
        }
    }
}
