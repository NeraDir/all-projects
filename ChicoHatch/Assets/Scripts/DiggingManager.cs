using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingManager : MonoBehaviour
{
    public float digRadius = 0.5f; 
    public float digStrength = 0.2f; 
    public LayerMask groundLayer; 

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    [SerializeField] private AudioClip _diggingClip;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        mesh = Instantiate(meshFilter.sharedMesh);
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh; 

        originalVertices = mesh.vertices;
        modifiedVertices = mesh.vertices;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector3 worldPoint = GetMouseWorldPosition();
            if (worldPoint != Vector3.zero)
            {
                Dig(worldPoint);
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            //SettingsManager.instance.onPlaySound?.Invoke(_diggingClip);
            return hit.point;
        }
        return Vector3.zero;
    }

    void Dig(Vector3 position)
    {
        for (int i = 0; i < modifiedVertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(originalVertices[i]);

            if (Vector3.Distance(worldVertex, position) < digRadius)
            {
                modifiedVertices[i] -= Vector3.up * digStrength; 
            }
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); 
        mesh.RecalculateBounds();

        meshCollider.sharedMesh = null; 
        meshCollider.sharedMesh = mesh; 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetMouseWorldPosition(), digRadius);
    }
}
