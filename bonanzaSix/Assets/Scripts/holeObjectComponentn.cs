using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeObjectComponentn : MonoBehaviour
{
    private Mesh myMesh;
    private Mesh mergeMesh;
    private Mesh holeMaker;

    [SerializeField]
    private MeshFilter myFilter;

    [SerializeField]
    private goodMergeComponent gooder;

    private MeshCollider _myCollider;

    public bool isLast;

    public void Init(Mesh myMeshInput,Mesh mergeMeshInput,Mesh holeMakerInput)
    {
        transform.parent.parent = null;
        _myCollider = GetComponent<MeshCollider>();
        myMesh = myMeshInput;
        mergeMesh = mergeMeshInput;
        holeMaker = holeMakerInput;
        myFilter.mesh = myMesh;
        
        transform.parent.rotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
        _myCollider.sharedMesh = myMesh;

    }

    private void LateUpdate()
    {
        gooder.Init(holeMaker, isLast);
    }

    public Mesh GetHoleMesh()
    {
        return holeMaker;
    }

    public Mesh GetMyMesh()
    {
        return myMesh;
    }
}
