using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjectsSpawnLine : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> pickUpObjectPrefabs;

    public void Init()
    {
        SpawnObjects();
    }

    private void OnEnable()
    {
        Init();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Instantiate(GetRandomPickUpObject(), spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

    private GameObject GetRandomPickUpObject()
    {
        return pickUpObjectPrefabs[Random.Range(0, pickUpObjectPrefabs.Count)];
    }
}
