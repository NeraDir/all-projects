using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> segmentsPatternList;



    public void Init(GameObject pickupObjectsLinePrefab, List<GameObject> obstaclePrefabs)
    {
        GameObject randPattern = segmentsPatternList[Random.Range(0, segmentsPatternList.Count)];

        randPattern.SetActive(true);

        if (randPattern.TryGetComponent(out PlatformPattern platformPattern))
        {
            platformPattern.SpawnObjects(pickupObjectsLinePrefab, obstaclePrefabs);
        }
  
    }

   

}

[System.Serializable]
public class PlatfromPointsData
{
    public List<Transform> points;

    public PlatfromPointsData()
    {
        points = new();
    }
}