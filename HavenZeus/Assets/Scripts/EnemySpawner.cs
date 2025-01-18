using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyTypes;
    [SerializeField]
    private Transform _enemyParentObject;
    [SerializeField]
    private Vector2 _spawnAreaSize;
    [SerializeField]
    private int _minEnemyCount;
    [SerializeField]
    private int _maxEnemyCount;

    private float _spawnPosX;
    private float _spawnPosY;

    public UnityEvent OnEnemySpawned;

    private Collider[] _objects;

    private void Start()
    {
        StartEnemySpawner();
    }
    public void SelectSpawnPosition()
    {
        _spawnPosX = Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2);
        _spawnPosY = Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2);
        Vector3 _spawnPos = new Vector3(_spawnPosX, -2f, _spawnPosY);

        CheckSpawnPosition(_spawnPos);
    }

    public void CheckSpawnPosition(Vector3 _spawnPosition)
    {
        _objects = Physics.OverlapBox(_spawnPosition, new Vector3(0.5f, 0.1f, 0.5f));

        if(_objects.Length > 0)
        {
            SelectSpawnPosition();
        }
        else
        {
            EnemySpawn(_spawnPosition);
        }
    }

    public void EnemySpawn(Vector3 spawnPos)
    {
        int enemyType = Random.Range(0, _enemyTypes.Length);

        Instantiate(_enemyTypes[enemyType], spawnPos, Quaternion.identity, _enemyParentObject);
    }

    public void StartEnemySpawner()
    {
        int enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount);

        for (int i = 0; i < enemyCount; i++)
        {
            SelectSpawnPosition();
            if (i == enemyCount - 1)
            {
                OnEnemySpawned?.Invoke();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_spawnAreaSize.x, 1.5f, _spawnAreaSize.y));
    }


}
