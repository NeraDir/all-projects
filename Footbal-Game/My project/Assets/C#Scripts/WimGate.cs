using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WimGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ball>() != null)
        {
            Win.instance.EndGame();
        }
    }
}
