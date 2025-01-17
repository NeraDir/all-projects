using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyEffect;

    private float damageValue;

    private bool canCollision = true;

    private Rigidbody m_RigidBody;

    private float liveTime = 6.0f;



    private void OnEnable()
    {
        Destroy(gameObject, liveTime);
        
    }

    public void Init(float damageValue)
    {
        this.damageValue = damageValue;
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Zombie zombie))
        {
            if (canCollision)
            {
                canCollision = false;
                //Destroy(zombie.gameObject.GetComponent<CapsuleCollider>());
                zombie.TakeDamage(damageValue);
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public Rigidbody GetRigidbodyComponent()
    {
        return m_RigidBody;
    }
}
