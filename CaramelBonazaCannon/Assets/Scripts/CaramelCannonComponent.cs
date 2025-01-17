using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaramelCannonComponent : MonoBehaviour
{
    private Transform _cannon;

    [SerializeField]
    private GameObject[] _canonBullet;

    [SerializeField]
    private Transform _cannonModel;

    [SerializeField]
    private GameObject _shootEffect;

    [SerializeField]
    private Transform _shootPosition;

    private float _shootinTime;

    private float _currentTimer;

    private void Start()
    {
        _cannon = GetComponent<Transform>();
        _shootinTime = CaramelCanonGameManager.CaramelCannonShootingTime;
        _currentTimer = 0;
    }

    private void LateUpdate()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >=_shootinTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _cannonModel.DOScaleY(80, 0.25f).OnComplete(() => _cannonModel.DOScaleY(100,0.25f).OnComplete(() =>
                {
                    Instantiate(_canonBullet[Random.Range(0, _canonBullet.Length)], _shootPosition.position, _shootPosition.rotation);
                    Instantiate(_shootEffect, _shootPosition.position, _shootPosition.rotation);
                    _currentTimer = 0;
                }));
            }
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            _cannon.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
        }
    }
}
