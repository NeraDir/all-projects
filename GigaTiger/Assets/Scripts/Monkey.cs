using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monkey : MonoBehaviour
{
    [SerializeField]
    private List<Transform> bananSpawnPoints;

    [SerializeField]
    private Transform bananSpawnPoint;

    [SerializeField]
    private Banan bananPrefab;

    [SerializeField]
    private MonkeyEntityAnimationsManager animationsManager;


    private int bananCount;


    private void OnEnable()
    {
        bananCount = Random.Range(1, bananSpawnPoints.Count);

        for(int i = 0; i < bananSpawnPoints.Count; i++)
        {
            bananSpawnPoints[i].gameObject.SetActive(false);
        }

        animationsManager.Init(this, bananCount);
    }

    public void StartAttackWithBanan()
    {
        animationsManager.ChangeToAttackAnimation();
    }

    public void SpawnBanan()
    {
        Banan newBanan = Instantiate(bananPrefab, bananSpawnPoint.position, bananPrefab.transform.rotation);

        Vector3 bonanDefaultSize = newBanan.transform.localScale;
        newBanan.transform.localScale = Vector3.zero;

        newBanan.transform.DOScale(bonanDefaultSize, 1f);
        newBanan.transform.DOMove(GetEmptySpawnPoint(), 0.6f);
    }


    private Vector3 GetEmptySpawnPoint()
    {
        Vector3 result = Vector3.zero;
        Transform pointInScene = bananSpawnPoints[Random.Range(0, bananSpawnPoints.Count)];


        while (pointInScene.gameObject.activeInHierarchy)
        {
            pointInScene = bananSpawnPoints[Random.Range(0, bananSpawnPoints.Count)];
        }
        pointInScene.gameObject.SetActive(true);

        result = pointInScene.position;

        return result;
    }


}
