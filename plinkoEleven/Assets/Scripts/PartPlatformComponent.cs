using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPlatformComponent : MonoBehaviour
{
    public Material myMaterial;

    [SerializeField]
    private Material[] _myMaterials;

    private MeshRenderer _myRenderer;

    public void Init()
    {
        _myRenderer = GetComponent<MeshRenderer>();
        myMaterial = myMaterial != null ? myMaterial : _myMaterials[Random.Range(0, _myMaterials.Length)];
        _myRenderer.material = myMaterial;
    }
}
