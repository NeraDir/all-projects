using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartsSpawner : MonoBehaviour
{
    public GameObject[] prefab;

    public static UnityEvent spawnPart = new UnityEvent();

    public GameObject lastPart;

    private void Start()
    {
        SpawnPart();
        spawnPart.AddListener(SpawnPart);
    }

    private void SpawnPart()
    {
        lastPart = Instantiate(prefab[Random.Range(0, prefab.Length)], new Vector3(lastPart.transform.position.x, lastPart.transform.position.y + 3000, 0), Quaternion.identity, lastPart.transform.parent);
        lastPart.transform.SetSiblingIndex(0);
        MoveObjectComponent.speed += 5;
    }
}
