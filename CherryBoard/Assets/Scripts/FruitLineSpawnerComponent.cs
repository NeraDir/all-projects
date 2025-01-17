using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLineSpawnerComponent : MonoBehaviour
{
    [SerializeField]
    private FruitsMoveDirections _fruitsMoveDirection;
    [SerializeField]
    private FruitComponente _fruitPrefab;

    private FruitComponente _lastFruit;

    private float _spawnDistance;

    private float crystalsMoveSpeed;

    public void Init(float crystalsMoveSpeed)
    {
        this.crystalsMoveSpeed = crystalsMoveSpeed;
        StartSpawn();
    }

    public void StartSpawn()
    {
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        FruitComponente tempFruit = Instantiate(_fruitPrefab, transform.position, transform.rotation, transform.parent);
        tempFruit.transform.SetSiblingIndex(0);
        tempFruit.Init(_fruitsMoveDirection, crystalsMoveSpeed);

        _lastFruit = tempFruit;

        _spawnDistance = _lastFruit.GetComponent<RectTransform>().sizeDelta.x;

        StartCoroutine(WaitSpawnNewFruit());
    }

    private IEnumerator WaitSpawnNewFruit()
    {
        while (Mathf.Abs(Vector2.Distance(transform.localPosition, _lastFruit.transform.localPosition)) < _spawnDistance)
        {
            yield return null;
        }
        SpawnFruit();
    }
}
