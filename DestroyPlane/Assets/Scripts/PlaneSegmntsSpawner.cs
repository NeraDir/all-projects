using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSegmntsSpawner : MonoBehaviour
{
    public Transform planeSegmentSpawnPoint;
    public PlanesSegment planeSegmentPrefab;

    public float planeSegemntSpeed;

    public List<Plane> planePrefabList;


    private PlanesSegment lastPlaneSegment;

    private RectTransform lastSegmentRectTransform;

    private float distanceToSpawn;

    private Coroutine waintToNextSpawnCoroutine;


    private void Awake()
    {
        SpawnPlaneSegment();
    }



    public void SpawnPlaneSegment()
    {
        PlanesSegment newPlanesSegemnt = Instantiate(planeSegmentPrefab, planeSegmentSpawnPoint.position, planeSegmentSpawnPoint.rotation, transform);
        newPlanesSegemnt.Init(planePrefabList);
        newPlanesSegemnt._speed = planeSegemntSpeed;
        lastPlaneSegment = newPlanesSegemnt;
        lastSegmentRectTransform = lastPlaneSegment.GetComponent<RectTransform>();

        
        distanceToSpawn = lastSegmentRectTransform.sizeDelta.y;

        waintToNextSpawnCoroutine = StartCoroutine(WaintToNextSpawn());
    }


   private IEnumerator WaintToNextSpawn()
    {
        while(Vector3.Distance(planeSegmentSpawnPoint.position, lastPlaneSegment.transform.position) < distanceToSpawn)
        {
            //Debug.Log(Vector3.Distance(planeSegmentSpawnPoint.position, lastPlaneSegment.transform.position));
            yield return null;
        }
        SpawnPlaneSegment();
    }
   
}
