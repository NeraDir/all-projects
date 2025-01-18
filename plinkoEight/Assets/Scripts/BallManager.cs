using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out StarManager star))
        {
            Destroy(star.gameObject);
        }
        if (collision.CompareTag("Finish"))
        {
            GameController.onShowResult?.Invoke(false);
        }
        if (collision.TryGetComponent(out SpikeManager spike))
        {
            GameController.onShowResult?.Invoke(true);
        }
    }
}
