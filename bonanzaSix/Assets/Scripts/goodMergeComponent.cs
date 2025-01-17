using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodMergeComponent : MonoBehaviour
{
    private MeshCollider _meshCollider;
    private MeshFilter _filter;

    private bool _isLast;

    public void Init(Mesh holeMaker,bool isLaster) 
    {
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = holeMaker;
        _meshCollider = GetComponent<MeshCollider>();
        _meshCollider.sharedMesh = _filter.sharedMesh;
        _isLast = isLaster;
    }

    public bool GetLast()
    {
        return _isLast;
    }
}
