using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddX : MonoBehaviour
{
    public CirclGameController ccController;
    public int x;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<GoUp>(out GoUp oo))
        {
            if (collision.GetComponent<Knife>().knifeSost == false)
            {
                ccController.AddX(x);
                collision.GetComponent<Knife>().knifeSost = true;

                collision.transform.SetParent(transform.parent);
                Destroy(oo);
            }
        }
    }
}
