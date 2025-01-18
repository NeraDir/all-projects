using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitJungleEggsComponent : MonoBehaviour
{
    private MeshRenderer _eggRenderer;

    [SerializeField]
    private Material[] _eggMaterials;

    private void Start()
    {
        _eggRenderer = GetComponent<MeshRenderer>();
        _eggRenderer.material = _eggMaterials[Random.Range(0, _eggMaterials.Length)];
    }

}
