using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Coin coin))
        {
            LevelHandler.levelCoinsCount++;
            Destroy(coin.transform.parent.gameObject);
        }
    }
}
