using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActiveState : BallBaseState
{
    private Color[] activeColors = { Color.red, Color.blue, Color.green, Color.magenta, Color.yellow };

    private bool isTriggered;

    public override void EnterState(Balls ball)
    {
        if (ball.gameManager.activeBalls.Find(x => x == ball))
        {
            return;
        }
        // change of color
        ball.GetComponent<MeshRenderer>().material.SetColor("_Color", activeColors[Random.Range(0, activeColors.Length)]);
        ball.tag = "Active";
        ball.gameManager.activeBalls.Add(ball);
    }

    public override void OnCollisionEnter(Balls ball, Collision collision)
    {
        
    }
}
