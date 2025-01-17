using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollide : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<GoUp>(out GoUp oo))
        {
            collision.transform.SetParent(parent);
            Destroy(oo);
        }
    }
}
