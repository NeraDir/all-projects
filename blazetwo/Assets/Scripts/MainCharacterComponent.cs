using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacterComponent : MonoBehaviour
{
    private Rigidbody _mainRigidBody;

    private Animator _animator;

    [SerializeField]
    private Material[] _skinMaterials;

    [SerializeField]
    private float _radius;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private TrailRenderer[] _trails;

    [SerializeField]
    private float _speed;

    private bool _onGround;
    private SkinnedMeshRenderer _meshRenderer;

    public static UnityEvent mainCharacterIsDead = new UnityEvent();
    public static UnityEvent mainCharacterIsFinished = new UnityEvent();

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        var mats = _meshRenderer.sharedMaterials;
        mats[0] = _skinMaterials[GameController.TopSkinIndex];
        mats[2] = _skinMaterials[GameController.BottomSkinIndex];
        _meshRenderer.sharedMaterials = mats;
        _mainRigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        _mainRigidBody.velocity = new Vector3(_mainRigidBody.velocity.x, _mainRigidBody.velocity.y, _speed);
        _onGround = Physics.CheckSphere(transform.position, _radius, _groundLayer);
        foreach (var item in _trails)
        {
            if (_onGround)
            {
                item.time = 0.35f;
            }
            else
            {
                item.time = 0;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("manstate", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("manstate", false);
        }
    }
}
