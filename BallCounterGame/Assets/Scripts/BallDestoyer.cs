using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestoyer : MonoBehaviour
{
    private int allBallCount;
    private int destroyedBallCount;

    private Collider2D lastBallCollider = null;

    public delegate void AllBallsDestoyedDelegate();
    public static event AllBallsDestoyedDelegate AllBallsDestoyedEvent;


    public void Init(int redBallCount, int greenBallCount, int blueBallCount)
    {
        allBallCount = redBallCount + greenBallCount + blueBallCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball) && lastBallCollider != collision)
        {
            lastBallCollider = collision;
            Destroy(ball.gameObject);
            destroyedBallCount++;


            if (destroyedBallCount == allBallCount)
            {
                if (AllBallsDestoyedEvent != null)
                    AllBallsDestoyedEvent();
            }

        }
    }
}
