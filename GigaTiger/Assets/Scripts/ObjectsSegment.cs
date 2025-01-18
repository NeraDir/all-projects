using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSegment : MonoBehaviour
{
    [SerializeField]
    private List<Transform> borderPoints;

    [SerializeField]
    private List<GameObject> platfromTypes;

    [SerializeField]
    private GameObject pickUpObjectLLinnePrefab;
    [SerializeField]
    private List<GameObject> segmentsPrefabs;

    public void Init()
    {
        GameObject randType = platfromTypes[Random.Range(0, platfromTypes.Count)];

        randType.SetActive(true);

        if (randType.TryGetComponent(out Platform platform))
        {
            platform.Init(pickUpObjectLLinnePrefab, segmentsPrefabs);
        }
    }

    public float GetLenght()
    {
        return Mathf.Abs(borderPoints[1].position.z - borderPoints[0].position.z);
    }
    
}
