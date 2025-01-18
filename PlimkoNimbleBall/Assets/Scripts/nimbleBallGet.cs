using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleBallGet : MonoBehaviour
{
    [SerializeField]
    private Material[] _nimbleBallMaterials;

    private MeshRenderer _nimbleMeshrenderer;

    public Transform _target;

    public bool isTriggered;

    private void Start()
    {
        _nimbleMeshrenderer = GetComponent<MeshRenderer>();
        _nimbleMeshrenderer.material = _nimbleBallMaterials[Random.Range(0,_nimbleBallMaterials.Length)];
    }

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, _target.position.y + 1, _target.position.z), 10 * Time.deltaTime);
    }
}
