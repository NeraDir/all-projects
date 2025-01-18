using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ConveyorComponent : MonoBehaviour
{
    private Material _material;

    private float _iteractions;

    [SerializeField]
    private Color _color;

    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private float _speed;

    private List<Rigidbody> _bodies = new List<Rigidbody>();

    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _material = Instantiate(mesh.materials[1]);
        mesh.sharedMaterials[1] = _material;
        _material = mesh.sharedMaterials[1];
    }

    private void LateUpdate()
    {
        if (_material != null)
        {
            _iteractions -= 1 * Time.deltaTime;
            if (_iteractions <= -10)
            {
                _iteractions = 0;
            }
            _material.color = _color;
            _material.mainTextureOffset = new Vector2(0, _iteractions);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = _direction * _speed;
    }
}
