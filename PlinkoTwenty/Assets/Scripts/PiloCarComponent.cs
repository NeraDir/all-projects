using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CarType
{
    idle,
    Ride,
    FastRide,
    ReverseRide
}
public class PiloCarComponent : MonoBehaviour,ITakebleComponent
{
    public CarType carType;

    [SerializeField]
    private Transform _refreshPlace;

    [SerializeField]
    private Material[] _carColors;

    [SerializeField]
    private MeshRenderer _renderer;

    [SerializeField]
    private GameObject _heartobject;
    [SerializeField]
    private GameObject _coinObject;

    private Vector3 _startPosition;

    private float _speed;

    public void Init()
    {
        if (Random.Range(0,2) != 0)
        {
            _coinObject.SetActive(true);
            _heartobject.SetActive(false);
        }
        else
        {
            _coinObject.SetActive(false);
            _heartobject.SetActive(true);
        }
        Material[] tempMaterials = _renderer.materials;
        tempMaterials[1] = _carColors[Random.Range(0, _carColors.Length)];
        _renderer.materials = tempMaterials;
        _startPosition = transform.localPosition;
        switch (carType)
        {
            case CarType.idle:
                transform.localPosition = new Vector3(0, 0.61f, _startPosition.z);
                break;
            case CarType.Ride:
                _speed = 6;
                break;
            case CarType.FastRide:
                _speed = 9;
                break;
            case CarType.ReverseRide:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _speed = 6;
                break;
        }
        if (carType != CarType.idle)
            StartCoroutine(Ride());
    }

    public void OnTake()
    {
        PiloGameManager.ballIsDead?.Invoke();
    }

    private IEnumerator Ride()
    {
        while (true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _refreshPlace.localPosition, _speed * Time.deltaTime);
            if (transform.localPosition == _refreshPlace.localPosition)
            {
                transform.localPosition = _startPosition;
            }
            yield return null;
        }
    }
}
