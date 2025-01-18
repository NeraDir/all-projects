using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPattern : MonoBehaviour
{
    [SerializeField]
    private List<Transform> pickUpObjectSpawnPoints;
    [SerializeField]
    private List<Transform> obstacleSpawnPoints;



    public void SpawnObjects(GameObject pickupObjectsLinePrefab, List<GameObject> obstaclePrefabs)
    {
        for(int i = 0; i < pickUpObjectSpawnPoints.Count; i++)
        {
            Instantiate(pickupObjectsLinePrefab, pickUpObjectSpawnPoints[i].position, Quaternion.identity);
        }

      

        for (int i = 0; i < obstacleSpawnPoints.Count; i++)
        {
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], obstacleSpawnPoints[i].position, Quaternion.identity);
        }
    }
}
