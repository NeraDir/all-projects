using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float _itemsMoveSpeed;

    [SerializeField] private bool _canMoveItems;
    
    private List<Rigidbody> _bodies = new List<Rigidbody>();
    
    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _material = Instantiate(mesh.materials[1]);
        mesh.sharedMaterials[1] = _material;
        _material = mesh.sharedMaterials[1];
        _material.color = _color;
        StartCoroutine(Working());
    }
    
    private IEnumerator Working()
    {
        while (true)
        {
            if (_material != null)
            {
                _iteractions -= _speed * Time.deltaTime;
                if (_iteractions <= -10)
                {
                    _iteractions = 0;
                }
                _material.mainTextureOffset = new Vector2(0, _iteractions);
            }
            yield return null;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if(!_canMoveItems)
            return;
        other.transform.position += _direction * _itemsMoveSpeed * Time.deltaTime;
    }
}
