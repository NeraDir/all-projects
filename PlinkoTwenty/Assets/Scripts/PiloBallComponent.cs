using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiloBallComponent : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _checkRadius;

    [SerializeField]
    private Material[] _ballColors;

    private MeshRenderer _meshRenderer;

    private bool _onTheGround;


    [SerializeField]
    private float _jumpStrenght;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _ballColors[Random.Range(0, _ballColors.Length)];
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (!PiloGameManager._gameRunned)
            return;
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, _speed);
        _onTheGround = Physics.CheckSphere(transform.position, _checkRadius, _layerMask);
        if (Input.GetMouseButtonDown(0))
        {
            if (_onTheGround)
                _rigidBody.AddForce(Vector3.up * _jumpStrenght, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITakebleComponent takable))
        {
            takable.OnTake();
        }
    }
}
