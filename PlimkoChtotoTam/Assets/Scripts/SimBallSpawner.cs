using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimBallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    public int rndBalls;

    private IEnumerator Start()
    {
        SimSaves.simCurrentScore = 0;
        rndBalls = Random.Range(10, 20);

        for (int i = 0; i < rndBalls; i++)
        {
            SimGameManager.ballsList.Add(Instantiate(ballPrefab, new Vector3(Random.Range(-2.36f, 2.36f), 2.91f, 0), Quaternion.identity));
            yield return new WaitForSeconds(0.3f);
            if (i == rndBalls -1)
            {
                StartCoroutine(FindObjectOfType<SimGameManager>().INIT());
            }
        }
    }
}
