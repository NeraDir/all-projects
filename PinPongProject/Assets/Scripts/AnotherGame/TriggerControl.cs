using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public int countScore = 0;
    public TriggerManager manag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ball"))
        {
            manag.ScoreCount += countScore;
        }
    }
}
