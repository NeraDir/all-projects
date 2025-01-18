using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObjectsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> firstLineSpawnPoints;


    [SerializeField]
    private GameObject treePrefab;
    [SerializeField]
    private GameObject coinPrefab;


    private void OnEnable()
    {
        FillLineWithObjects(firstLineSpawnPoints);
    }


    public void FillLineWithObjects(List<Transform> linePoints)
    {

        for (int i = 0; i < firstLineSpawnPoints.Count; i++)
        {
            linePoints[i].gameObject.SetActive(false);
        }



        int treeCount = Random.Range(0, linePoints.Count - 1);
        Transform buff_point = null;


        if (treeCount != 0)
        {
            int currentTreeCount = 0;

            

            while (currentTreeCount != treeCount)
            {
                int randSpawnPointIndex = Random.Range(0, linePoints.Count);

                if (!linePoints[randSpawnPointIndex].gameObject.activeInHierarchy)
                {
                    linePoints[randSpawnPointIndex].gameObject.SetActive(true);
                    buff_point = linePoints[randSpawnPointIndex];

                    Instantiate(treePrefab, buff_point.position, buff_point.rotation, buff_point);
                    currentTreeCount++;
                }
            }


        }

        for (int i = 0; i < linePoints.Count; i++)
        {

            if (Random.Range(0, 101) <= 70)
            {
                if (!linePoints[i].gameObject.activeInHierarchy)
                {
                    linePoints[i].gameObject.SetActive(true);
                    buff_point = linePoints[i];
                    Instantiate(coinPrefab, buff_point.position, buff_point.rotation, buff_point);
                }
            }
        }



    }
}
