using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSlotItem : MonoBehaviour
{
    public int itemIndex;

    public Mesh[] itemMeshes;

    public Material[] itemMaterials;

    private MeshRenderer itemMeshRenderer;

    private MeshFilter itemMeshFilter;

    private void Start()
    {
        itemMeshRenderer = GetComponent<MeshRenderer>();
        itemMeshFilter = GetComponent<MeshFilter>();
    }

    public void Init()
    {
        itemIndex = Random.Range(0, itemMeshes.Length);
        itemMeshFilter.mesh = itemMeshes[itemIndex];
        itemMeshRenderer.material = itemMaterials[itemIndex];
        GetComponent<MaskingComponent>().UpdateVisual();
    }
}
