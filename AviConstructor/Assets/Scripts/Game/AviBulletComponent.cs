using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviBulletComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position += new Vector3(0, 1,0) * 1200 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AviBombComponent bomb))
        {
            bomb.bombHealth -= AviGameComponent.currentAviDamage;
            if (bomb.bombHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
