using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadStarsSpawner : MonoBehaviour
{
    public GameObject star;

    public Transform[] starsSpawnBorders;

    public GameObject[] winds;

    public void SpawnStars()
    {
       MadStarComponent[] stars =  FindObjectsOfType<MadStarComponent>();
        if (stars.Length > 0 )
        {
            foreach (var item in stars)
            {
                item.DestroyMe();
            }
        }
        for (int i = 0; i < Random.Range(2,5); i++)
            Instantiate(star, new Vector2(Random.Range(starsSpawnBorders[0].position.x, starsSpawnBorders[1].position.x), Random.Range(starsSpawnBorders[0].position.y, starsSpawnBorders[1].position.y)), Quaternion.Euler(0, 0, Random.Range(-360, 360)));

        foreach (var item in winds)
        {
            item.SetActive(false);
        }
        int rndWind = Random.Range(0, winds.Length);
        winds[rndWind].SetActive(true);
        winds[rndWind].transform.position = new Vector3(Random.Range(starsSpawnBorders[0].position.x, starsSpawnBorders[1].position.x), winds[rndWind].transform.position.y, winds[rndWind].transform.position.z);
    }
}
