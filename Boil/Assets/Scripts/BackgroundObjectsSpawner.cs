using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectsSpawner : MonoBehaviour
{
    
    public List<Transform> spawnPoints;
    public List<GameObject> prefabs;

    public float spawnTime;

    private Coroutine ObjectSpawnerCoroutine;



    private void Start()
    {
        StartSpawn();
    }


    public void StartSpawn()
    {
        ObjectSpawnerCoroutine = StartCoroutine(objectSpawner());
    }
    public void StopSpawn()
    {
        if (ObjectSpawnerCoroutine != null)
            StopCoroutine(ObjectSpawnerCoroutine);
    }


    private IEnumerator objectSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);

    }

}
