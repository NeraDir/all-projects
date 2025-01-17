using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesSegment : MonoBehaviour
{
    public float _speed = 0;

    private List<Plane> planePrefabList;


    [SerializeField]
    private List<Transform> planeSpawnPoints;


    public void Init(List<Plane> planePrefabList)
    {
        this.planePrefabList = planePrefabList;
        SpawnPlanes();
    }

    public void SpawnPlanes()
    {
        for (int i = 0; i < planeSpawnPoints.Count; i++)
        {
            Instantiate(GetRandomPlane(), planeSpawnPoints[i].position, planeSpawnPoints[i].rotation, planeSpawnPoints[i]);
        }
    }

    public Plane GetRandomPlane()
    {
        return planePrefabList[Random.Range(0, planePrefabList.Count)];
    }

    void FixedUpdate()
    {
        transform.position -= Vector3.up * _speed;
    }
}
