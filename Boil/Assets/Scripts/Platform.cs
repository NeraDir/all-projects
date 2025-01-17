using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    public bool isFixed;
    public bool isHasBackGroundObjects;

    public float xAxisMoveRange;

    private float xAxisMaxRightPos;
    private float xAxisMaxLefttPos;

    public List<GameObject> backgroundObjectsPrefabs;
    public List<Transform> backGroundSpawnPoints;


    public float direction = 1;

    private void OnEnable()
    {

        if (!isFixed)
        {
            SetMovement();
        }

        if (isHasBackGroundObjects)
        {
            if (!isFixed)
                return;

            HideAllSpawnPoints();
            SpawnBackGroundObjects();
        }

        //transform.DOMoveX();
    }


    private void SetMovement()
    {

        if (direction == -1)
        {
            xAxisMaxLefttPos = transform.position.x - xAxisMoveRange;
            transform.position = transform.position + (Vector3.right * xAxisMoveRange);
            xAxisMaxRightPos = transform.position.x;


            Sequence moveSequence = DOTween.Sequence();

            moveSequence.Append(transform.DOMoveX(xAxisMaxLefttPos, 3));
            moveSequence.Append(transform.DOMoveX(xAxisMaxRightPos, 3));
            moveSequence.SetLoops(-1, LoopType.Restart);
        }
        else
        {
            xAxisMaxRightPos = transform.position.x + xAxisMoveRange;
            transform.position = transform.position - (Vector3.right * xAxisMoveRange);
            xAxisMaxLefttPos = transform.position.x;


            Sequence moveSequence = DOTween.Sequence();

            moveSequence.Append(transform.DOMoveX(xAxisMaxRightPos, 3));
            moveSequence.Append(transform.DOMoveX(xAxisMaxLefttPos, 3));
            moveSequence.SetLoops(-1, LoopType.Restart);
        }
    }

    private void SpawnBackGroundObjects()
    {
        for (int i = 0; i < backGroundSpawnPoints.Count; i++)
        {
            if(Random.Range(0, 101) > 50)
            {

                Transform spawnPoint = GetEmptySpawnPoint();

                Instantiate(backgroundObjectsPrefabs[Random.Range(0, backgroundObjectsPrefabs.Count)], spawnPoint.position, Quaternion.identity);
            }
        }
    }


    private void HideAllSpawnPoints()
    {
        for (int i = 0; i < backGroundSpawnPoints.Count; i++)
        {
            backGroundSpawnPoints[i].gameObject.SetActive(false);
        }
    }

    private Transform GetEmptySpawnPoint()
    {
        Transform result = backGroundSpawnPoints[Random.Range(0, backGroundSpawnPoints.Count)];

        while (result.gameObject.activeInHierarchy)
        {
            result = backGroundSpawnPoints[Random.Range(0, backGroundSpawnPoints.Count)];
        }

        return result;
    }

}
