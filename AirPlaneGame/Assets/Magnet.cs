using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collectable coll))
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, 2.1f*Time.deltaTime);
        }
    }
}
