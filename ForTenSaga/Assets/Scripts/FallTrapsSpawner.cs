using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrapsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _fallTrapPrefab;
    
    [SerializeField] private Transform[] _spawnPoints;

    public void Init()
    {
        StartCoroutine(SpawningTraps());
    }
    
    private IEnumerator SpawningTraps()
    {
        while (true)
        {
            if (Random.Range(0,2) != 0)
            {
                Instantiate(_fallTrapPrefab, new Vector3(
                    Random.Range(_spawnPoints[0].position.x,_spawnPoints[1].position.x),
                    _spawnPoints[0].position.y,
                    _spawnPoints[0].position.z
                ), _fallTrapPrefab.transform.rotation);
            }
            yield return new WaitForSeconds(4f);
        }
    }
}
