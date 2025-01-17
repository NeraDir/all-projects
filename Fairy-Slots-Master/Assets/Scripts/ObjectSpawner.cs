using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _coinsSpawnPoints;
    [SerializeField]
    private GameObject _coinPrefabs;


    [SerializeField]
    private List<Transform> _bgSpawnPoints;
    [SerializeField]
    private List<GameObject> _bgPrefabs;




    private void OnEnable()
    {
        StartCoroutine(spawnCoins());
        StartCoroutine(spawnBGPoints());
    }


    private IEnumerator spawnCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 4f));

            if (MainGameManager.canSpawnObjects)
            {
                Transform buffPoint = _coinsSpawnPoints[Random.Range(0, _coinsSpawnPoints.Count)];
                Instantiate(_coinPrefabs, buffPoint.position, buffPoint.rotation);
            }

            
        }
    }

    private IEnumerator spawnBGPoints()
    {
        while (true)
        {
            if (MainGameManager.canSpawnObjects)
            {
                Transform buffPoint = _bgSpawnPoints[Random.Range(0, _bgSpawnPoints.Count)];
                Instantiate(_bgPrefabs[Random.Range(0, _bgSpawnPoints.Count)], buffPoint.position, buffPoint.rotation);
            }

            yield return new WaitForSeconds(Random.Range(3f, 4f));

        }
    }

}
