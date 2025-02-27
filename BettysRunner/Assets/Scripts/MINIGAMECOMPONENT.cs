using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MINIGAMECOMPONENT : MonoBehaviour
{
    [SerializeField] private MINIGAMEITEMCOMPONENT _itemPrefab;

    [SerializeField] private Transform[] _spawnPosition;

    private int _maxCount = 20;

    private List<MINIGAMEITEMCOMPONENT> _itemsPool = new List<MINIGAMEITEMCOMPONENT>();

    private IEnumerator Start()
    {
        while (_itemsPool.Count < _maxCount)
        {
            yield return new WaitForSeconds(0.5f);
            _itemsPool.Add(SpawnItem());
        }
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            OnRespawn();
        }
    }

    private void OnRespawn()
    {
        if (_itemsPool.Count < _maxCount)
            return;
        MINIGAMEITEMCOMPONENT newItem = _itemsPool.FindLast(x => !x.gameObject.activeInHierarchy);
        if (newItem != null)
        {
            newItem.gameObject.SetActive(true);
            newItem.transform.position = GetSpawnPosition();
        }
    }

    private MINIGAMEITEMCOMPONENT SpawnItem()
    {
        MINIGAMEITEMCOMPONENT newItem = Instantiate(_itemPrefab, GetSpawnPosition(), Quaternion.identity);
        newItem.transform.SetParent(_spawnPosition[0].parent);
        newItem.transform.SetSiblingIndex(0);
        return newItem;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomPosition = new Vector3(
            Random.Range(_spawnPosition[0].position.x, _spawnPosition[1].position.x),
            Random.Range(_spawnPosition[0].position.y, _spawnPosition[1].position.y),
            0);
        return randomPosition;
    }
}
