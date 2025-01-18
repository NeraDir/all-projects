using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallComponent : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private int _rocksMinCount = 1;
    [SerializeField] private int _rocksMaxCount = 3;

    [SerializeField] private GameObject[] _rockPrefabs;
    
   [SerializeField] private float _minDistanceBetweenObjects = 1f; // Минимальное расстояние между объектами

    private List<Vector2> _spawnedPositions = new List<Vector2>(); // Список занятых позиций

    private List<GameObject> _wallElements = new List<GameObject>();
    
    private void Awake()
    {
        SpawnRocks();
    }

    private void OnDestroy()
    {
        foreach (var item in _wallElements)
        {
            Destroy(item.gameObject);
        }
        _wallElements.Clear();
    }

    private void SpawnRocks()
    {
        int rndCount = Random.Range(_rocksMinCount, _rocksMaxCount);

        for (int i = 0; i < rndCount; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();

            // Если найдена валидная позиция, спавним объект
            if (spawnPosition != Vector3.zero)
            {
                GameObject rockPrefab = _rockPrefabs[Random.Range(0, _rockPrefabs.Length)];
                _wallElements.Add(Instantiate(rockPrefab, spawnPosition, rockPrefab.transform.rotation));

                // Сохраняем позицию как занятую
                _spawnedPositions.Add(new Vector2(spawnPosition.x, spawnPosition.y));
            }
            else
            {
                Debug.LogWarning("Не удалось найти подходящую позицию для объекта.");
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        int maxAttempts = 100; // Количество попыток найти подходящую позицию
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Генерируем случайную позицию в пределах границ
            float randomX = Random.Range(_spawnPoints[0].position.x, _spawnPoints[1].position.x);
            float randomY = Random.Range(_spawnPoints[0].position.y, _spawnPoints[1].position.y);

            Vector2 potentialPosition = new Vector2(randomX, randomY);

            // Проверяем, что позиция валидна
            if (IsPositionValid(potentialPosition))
            {
                return new Vector3(potentialPosition.x, potentialPosition.y, _rockPrefabs[0].transform.position.z);
            }
        }

        return Vector3.zero; // Возвращаем 0, если не удалось найти позицию
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (var existingPosition in _spawnedPositions)
        {
            if (Vector2.Distance(position, existingPosition) < _minDistanceBetweenObjects)
            {
                return false; // Слишком близко к другому объекту
            }
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Рисуем границы спавна
        if (_spawnPoints.Length >= 2)
        {
            Gizmos.DrawLine(_spawnPoints[0].position, _spawnPoints[1].position);
        }

        // Рисуем занятые позиции, если игра запущена
        Gizmos.color = Color.red;
        foreach (var position in _spawnedPositions)
        {
            Gizmos.DrawSphere(new Vector3(position.x, position.y, 0), 1f);
        }
    }
}
