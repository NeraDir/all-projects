using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /*
    public delegate void ObstacleDetecter();
    public static event ObstacleDetecter ObstacleDetected;
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            if (ObstacleDetected != null)
            {
                ObstacleDetected();
            }
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {

            if (ObstacleDetected != null)
            {
                ObstacleDetected();
            }
        }
        */
    }

}
