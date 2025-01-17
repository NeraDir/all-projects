using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Zombie zombiePrefab;

    [SerializeField]
    private float zombieSpawnTime;
    [SerializeField]
    private float maxSpawnRadius;
    private float spawnRadius;

    private Vector3 newZombieSpawnPos;
    private float xAxis;
    private float zAxis;

    private Coroutine spawnCoroutine;

    private List<Zombie> buff_ZombieInSceneList;

    private void OnEnable()
    {
        StartCoroutine(SpawnZombie());
    }


    private IEnumerator SpawnZombie()
    {
        Zombie newZombie = null;
        while(true)
        {
            spawnRadius = Random.Range(20, maxSpawnRadius);
            zAxis = Random.Range(-spawnRadius, spawnRadius);
            xAxis = Mathf.Sqrt(Mathf.Pow(spawnRadius, 2) - Mathf.Pow(zAxis, 2));

            newZombieSpawnPos = new Vector3(Random.Range(0, 2) == 1 ? xAxis : -xAxis, 0, zAxis);
            newZombie = Instantiate(zombiePrefab, newZombieSpawnPos, zombiePrefab.transform.rotation);
            newZombie.Init(GetNewZombieLevel(),transform);

            yield return new WaitForSeconds(zombieSpawnTime);
        }
    }

    public void StartZombiesSpawn()
    {
        spawnCoroutine = StartCoroutine(SpawnZombie());
    }
    public void StopZombieSpawn()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        
    }

    public void FreezeAllZombieInScene()
    {
        buff_ZombieInSceneList = FindObjectsOfType<Zombie>().ToList();

        for (int i = 0; i < buff_ZombieInSceneList.Count; i++)
        {
            buff_ZombieInSceneList[i].FreezeAnimation();
        }
        
    }
    public void UnFreezeAllZombieInScene()
    {
        buff_ZombieInSceneList = FindObjectsOfType<Zombie>().ToList();

        for (int i = 0; i < buff_ZombieInSceneList.Count; i++)
        {
            buff_ZombieInSceneList[i].ContinuePlayAnimation();
        }

        buff_ZombieInSceneList.Clear();
    }

    public int GetNewZombieLevel()
    {
        if (GameSceneController.levelNumber == 1)
            return 1;
        else
            return Random.Range(GameSceneController.levelNumber, GameSceneController.levelNumber + 1);
    }
}
