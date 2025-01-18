using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformComponent : MonoBehaviour
{
    public List<Material> _platformMaterials;

    [SerializeField]
    private MeshRenderer[] _platformRenderers;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private GameObject[] trapObjects;

    private void Start()
    {
        for (int i = 0; i < _platformRenderers.Length; i++)
        {
            _platformRenderers[i].material = _platformMaterials[i];
        }
        foreach (var item in spawnPositions)
        {
            if (Random.Range(0,4) == 1)
            {
                Instantiate(trapObjects[Random.Range(0, trapObjects.Length)], item.position, Quaternion.identity);
            }
        }
    }
}
