using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private List<BG_SegmentController> segmentPrefab;


    [SerializeField]
    private List<BG_SegmentController> easy_SegmentPrefabs;
    [SerializeField]
    private List<BG_SegmentController> medium_SegmentPrefabs;
    [SerializeField]
    private List<BG_SegmentController> hard_SegmentPrefabs;

    private BG_SegmentController lastSegment;

    private Vector3 spawnPos;
    private int segmentCountInScene;

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        BallDetecter.ExitBallLastSegmentWasFixed += SpawnSegment;

        StartSpawnSegments();
       

    }
    private void OnDisable()
    {
        BallDetecter.ExitBallLastSegmentWasFixed -= SpawnSegment;
    }

    public void StartSpawnSegments()
    {
        segmentCountInScene = 0;
        spawnPos = Vector3.zero;


        lastSegment = Instantiate(segmentPrefab[0], spawnPos, segmentPrefab[0].transform.rotation);
        spawnPos = new Vector3(0, lastSegment.transform.position.y + (lastSegment.upPoint.position.y - lastSegment.downPoint.position.y), 0);
        segmentCountInScene++;


        for (int i = 0; i < 5; i++)
        {
            SpawnSegment();
        }
    }

    public void SpawnSegment()
    {
        lastSegment = Instantiate(GetSegment(), spawnPos, segmentPrefab[1].transform.rotation);
        spawnPos = new Vector3(0, lastSegment.transform.position.y + (lastSegment.upPoint.position.y - lastSegment.downPoint.position.y), 0);
        segmentCountInScene++;
    }


    private BG_SegmentController GetSegment()
    {
        BG_SegmentController result = null;


        if (segmentCountInScene < 3)
        {
            result = GetRandomSegment(easy_SegmentPrefabs);
        }
        else if (segmentCountInScene >= 3 && segmentCountInScene < 8)
        {
            result = GetRandomSegment(medium_SegmentPrefabs);
        }
        else if(segmentCountInScene >= 8)
        {
            result = GetRandomSegment(hard_SegmentPrefabs);
        }


        Debug.Log("segmentCountInScene: " + segmentCountInScene);


        return result;
    }

    private BG_SegmentController GetRandomSegment(List<BG_SegmentController> segmentPrefabsList)
    {
        BG_SegmentController result = segmentPrefabsList[Random.Range(0, segmentPrefabsList.Count)];

        if (lastSegment.index == result.index && lastSegment.segmentType == result.segmentType)
        {
            while (lastSegment.index != result.index)
            {
                result = segmentPrefabsList[Random.Range(0, segmentPrefabsList.Count)];
            }
        }

        return result;
    }
}
