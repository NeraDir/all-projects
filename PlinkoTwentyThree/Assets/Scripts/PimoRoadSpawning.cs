using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoRoadSpawning : MonoBehaviour
{
    [SerializeField]
    private Transform _cup;

    [SerializeField]
    private Transform _roadPref;

    [SerializeField]
    private Transform _roadChek;

    [SerializeField]
    private Transform _roadSpawnPos;

    [SerializeField]
    private Transform _roadDestroy;

    [SerializeField]
    private PimoTargetMove _targetMovePref;

    [SerializeField]
    private Transform _checkPosition;

    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private Transform _deletePosition;

    private bool _isInitializated;

    private PimoTargetMove _lastTarget;

    private Transform _lastRoad;

    private void Awake()
    {
        _isInitializated = false;
        PimoGameController.gameInitialization.AddListener(OnInitialization);
    }

    private void OnDestroy()
    {
        PimoGameController.gameInitialization.RemoveListener(OnInitialization);
    }

    private void OnInitialization() => _isInitializated = true;

    private IEnumerator Start()
    {
        StartCoroutine(SpawnRoad());
        while (true)
        {
            if (_isInitializated)
            {
                if (_lastTarget != null)
                {
                    if (_lastTarget.transform.position.z >= _checkPosition.position.z)
                    {
                        PimoTargetMove tempMover = Instantiate(_targetMovePref, _spawnPosition.position, Quaternion.identity);
                        tempMover.Init(_cup, _deletePosition);
                        _lastTarget = tempMover;
                    }
                }
                else
                {
                    PimoTargetMove tempMover = Instantiate(_targetMovePref, _spawnPosition.position, Quaternion.identity);
                    tempMover.Init(_cup, _deletePosition);
                    _lastTarget = tempMover;
                }
                
            }
            yield return null;
        }
    }

    private IEnumerator SpawnRoad()
    {
        while (true)
        {
            if (_isInitializated)
            {
                if (_lastRoad != null)
                {
                    if (_lastRoad.transform.position.z >= _roadChek.position.z)
                    {
                        Transform tempMover = Instantiate(_roadPref, _roadSpawnPos.position, Quaternion.identity);
                        _lastRoad = tempMover;
                    }
                }
                else
                {
                    Transform tempMover = Instantiate(_roadPref, _roadSpawnPos.position, Quaternion.identity);
                    _lastRoad = tempMover;
                }

            }
            yield return null;
        }
    }
}
