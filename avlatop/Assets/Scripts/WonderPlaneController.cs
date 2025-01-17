using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderPlaneController : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    private Rigidbody _rigidbody;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _rotor;

    private MeshRenderer[] _renderers;

    [SerializeField]
    private Material[] _materials;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void LateUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * 5, _rigidbody.velocity.y, _rigidbody.velocity.z);
        _rotor.Rotate(new Vector3(-1, 0, 0), 1260 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollisionableComponent collison))
        {
            collison.Use(_target);
        }
    }

    public void GetDamage() 
    {
        StartCoroutine(Damaging());
    }

    private IEnumerator Damaging() 
    {
        foreach (var item in _renderers)
        {
            item.material = _materials[1];
        }
        yield return new WaitForSeconds(0.1f);
        GameManager.wonderPlaneHealth -= 25f;
        foreach (var item in _renderers)
        {
            item.material = _materials[0];
        }
    }
}
