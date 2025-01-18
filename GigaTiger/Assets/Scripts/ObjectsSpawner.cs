using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform firstObjectSegment;
    [SerializeField]
    private ObjectsSegment objectsSegmentPrefab;

    [SerializeField]
    private FinalSegment finalSegmentPrefab;

    private float segmentLenght;
    private Vector3 newSegmentPosition;


    private int levelLenght;
    private int platfromCount;

    private void Init()
    {
        segmentLenght = objectsSegmentPrefab.GetLenght();
        newSegmentPosition = firstObjectSegment.position;
        newSegmentPosition.z += segmentLenght;

        if (levelLenght < 10)
        {
            levelLenght = GamePlayController.currentLevelNumber + 4;
        }
        else
        {
            levelLenght = 15;
        }

    }

    private void Start()
    {
        Init();

        for (int i = 0; i < levelLenght; i++)
        {
            SpawnNewSegment();
        }

        SpawnFinalSegment();
    }


    public void SpawnNewSegment()
    {
        ObjectsSegment newObjectsSegment = Instantiate(objectsSegmentPrefab, newSegmentPosition, objectsSegmentPrefab.transform.rotation);
        newObjectsSegment.Init();
        newSegmentPosition.z += segmentLenght;


    }

    private void SpawnFinalSegment()
    {
        newSegmentPosition.z -= segmentLenght/2;
        newSegmentPosition.z += finalSegmentPrefab.GetLenght()/2;

        FinalSegment finalSegment = Instantiate(finalSegmentPrefab, newSegmentPosition, Quaternion.identity);
    }


}
