using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] floors;

    public GameObject currentFloor;

    public void SpawnFloor() 
    {
        currentFloor = Instantiate(floors[Random.Range(0, floors.Length)], new Vector3(currentFloor.transform.position.x, currentFloor.transform.position.y, currentFloor.transform.position.z + 980), Quaternion.identity);
    }
}
