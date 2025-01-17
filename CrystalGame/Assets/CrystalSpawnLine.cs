using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalSpawnLine : MonoBehaviour
{


    private Direction direction;
    private List<Crystal> crystalPrefabs;


    private Crystal lastCrystal;
    private RectTransform lastCrystalImageRectTransform;

    private float distanceToSpawn;
    private Coroutine waintToNextSpawmCor;

    private float crystalsMoveSpeed;

    private Vector3 mPosition;





    public void Init(List<Crystal> crystalPrefabs, Direction direction, float crystalsMoveSpeed)
    {
        this.crystalPrefabs = crystalPrefabs;
        this.direction = direction;
        this.crystalsMoveSpeed = crystalsMoveSpeed;

        mPosition = transform.position;
    }


    public void StartSpawn()
    {
        SpawnCrystal();
    }

    private void SpawnCrystal()
    {

        Crystal newCrystal = Instantiate(GetRandomCrystal(), transform.position, transform.rotation, transform.parent);
        newCrystal.Init(direction, crystalsMoveSpeed);

        lastCrystal = newCrystal;
        lastCrystalImageRectTransform = lastCrystal.GetComponent<RectTransform>();

        distanceToSpawn = lastCrystalImageRectTransform.sizeDelta.x;

        StartCoroutine(WaintToNextSpawn());
    }

    private IEnumerator WaintToNextSpawn()
    {
        while (Mathf.Abs(Vector2.Distance(transform.localPosition, lastCrystal.transform.localPosition)) < distanceToSpawn)
        {
            yield return null;
        }
        SpawnCrystal();
    }


    private Crystal GetRandomCrystal()
    {
        return crystalPrefabs[Random.Range(0, crystalPrefabs.Count)];
    }
}

public enum Direction
{
    Right,
    Left
}