using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimBallComponent : MonoBehaviour
{
    public GameObject greatTxt;

    public GameObject looseTxt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SimPlaceWithTheScore place))
        {
            SimSaves.simCurrentScore += place.value;
            if (SimSaves.simCurrentScore > SimSaves.simBestScore)
            {
                SimSaves.simBestScore = SimSaves.simCurrentScore;
            }
            Instantiate(greatTxt, transform.position, Quaternion.identity);
            SimGameManager.ballsList.Remove(this.gameObject);
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out SimDestroyerWall wall))
        {
            Instantiate(looseTxt, transform.position, Quaternion.identity);
            SimGameManager.ballsList.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}
