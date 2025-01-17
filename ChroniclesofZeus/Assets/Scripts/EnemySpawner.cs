using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> allEnemiesPrefabs;

    [SerializeField]
    private List<Transform> enemySpawnPoint;

    [SerializeField]
    private float enemySpawnTime;

    [SerializeField]
    private Zues Zues;

    public static List<Enemy> allEnemyInScene;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }
    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Transform buff_spawnPoint = enemySpawnPoint[Random.Range(0, enemySpawnPoint.Count)];
        Enemy buff_enemy = Instantiate(allEnemiesPrefabs[Random.Range(0, allEnemiesPrefabs.Count)], buff_spawnPoint.position, buff_spawnPoint.rotation);
        buff_enemy.target = Zues.transform;

    }
}
