using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void Awake()
    {
        Rigidbody m_BulletBody = gameObject.AddComponent<Rigidbody>();
        m_BulletBody.AddForce(transform.forward * 150,ForceMode.Impulse);
        Destroy(gameObject, 10);
    }
}
