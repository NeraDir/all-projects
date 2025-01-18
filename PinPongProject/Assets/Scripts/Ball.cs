using UnityEngine;

public class Ball : MonoBehaviour
{
    public PingPong pingPomg;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Target"))
        {
            pingPomg.DecreaseScores();
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("WallDown"))
        {
            if (pingPomg != null)
                pingPomg.Lose();
        }
    }
}