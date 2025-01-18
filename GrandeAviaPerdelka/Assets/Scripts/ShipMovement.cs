using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour, IRocketDieble
{
    private Rigidbody shipBody;

    [SerializeField]
    private float shipMovementSpeed;

    private Transform _target;

    [SerializeField]
    private float _rotatingSpeed;

    [SerializeField]
    private GameObject _rocket;

    [SerializeField]
    private Transform _rocketSpawn;

    [SerializeField]
    private GameObject _shootEffect;

    private IEnumerator Start()
    {
        _target = FindObjectOfType<PlanerController>().transform;
        shipBody = GetComponent<Rigidbody>();

        Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;

        direction.Normalize();
        if (direction.x > 0.1)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -270, 0);
        }

        while (true)
        {
            
            yield return new WaitForSeconds(5);
            Instantiate(_shootEffect, transform);
            GameObject rocker = Instantiate(_rocket, _rocketSpawn.position, _rocketSpawn.rotation);
            rocker.transform.rotation = Quaternion.Euler(-90,0,111);
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
            shipBody.velocity = -transform.forward * shipMovementSpeed;
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }
}
