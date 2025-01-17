using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController EnemyPrefab;
    public float SpawnTime = 2f;

    private float Timer = 2f;

    private void Update()
    {
        if(!CelestialGameManager.Instance.GameStarted) return;

        Timer += Time.deltaTime;

        if(Timer >= SpawnTime)
        {
            SpawnEnemy();
            Timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int rnd = Random.Range(0, CelestialGameManager.Instance.SpawnPoses.Count);
        var enemy = Instantiate(EnemyPrefab, CelestialGameManager.Instance.SpawnPoses[rnd].Pos.position, Quaternion.identity, CelestialGameManager.Instance.Parrent);
        enemy.INIT(CelestialGameManager.Instance.SpawnPoses[rnd].ID);
    }
}