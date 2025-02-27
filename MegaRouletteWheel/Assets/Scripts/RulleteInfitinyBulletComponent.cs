using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulleteInfitinyBulletComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position += transform.forward * 3 * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent(out RouletteInfinityJellyComponent jelly))
            {
                Destroy(gameObject);
                jelly.JellDestroy();
            }
        }
    }
}
