using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHero : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; 
    public Transform[] spawnPositions;
    public float spawnDelay = 1.0f;


    void Start()
    {        
        InvokeRepeating("SpawnObjAgain", 0f, 7.0f);
    }
    void SpawnObjAgain()
    {
        if (!EndGame.endGame)
            for (int i = 0; i < prefabsToSpawn.Length; i++)
                SpawnPrefab(prefabsToSpawn[i], spawnPositions[i]);
    }
    void SpawnPrefab(GameObject prefab, Transform spawnPosition)
    {
        // Создаем экземпляр префаба на указанной позиции.
        Instantiate(prefab, spawnPosition.position, Quaternion.identity, spawnPosition.transform);
    }
}
