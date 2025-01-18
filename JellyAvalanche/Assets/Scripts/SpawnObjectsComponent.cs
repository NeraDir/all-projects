using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsComponent : MonoBehaviour
{
    public Material material;
    
    [SerializeField]
    private Transform[] _spawnPoints;
    
    private GameObject _objectPrefab;
    
    public void Init(GameObject prefab)
    {
        _objectPrefab = prefab;
    }

    public void SpawnObjects()
    {
        foreach (var item in _spawnPoints)
        {
            GameObject newObject = Instantiate(_objectPrefab, transform.position, Quaternion.identity);
            newObject.GetComponent<MeshRenderer>().material = material;
        }
    }
}
