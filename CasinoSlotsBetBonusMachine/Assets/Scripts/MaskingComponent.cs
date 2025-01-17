using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingComponent : MonoBehaviour
{
    MeshRenderer MeshRenderer;

    private void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MeshRenderer.material.renderQueue = 3002;
    }
}
