using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadspawnercomponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _road;

    [SerializeField]
    private GameObject _withoutRoad;

    [SerializeField]
    private GameObject _clearRoad;

    [SerializeField]
    private GameObject _withoutRoadEnd;

    [SerializeField]
    private GameObject _roadEnd;

    private GameObject _lastRoad;

    private int _interation;

    private bool _canIteration;

    private void Start()
    {
        _interation = 0;
        for (int i = 0; i < 5; i++)
        {
            SpawnRoad();
        }
        playercontroller.RoadSpawn.AddListener(SpawnRoad);
        _canIteration = true;
    }

    private void OnDestroy()
    {
        playercontroller.RoadSpawn.RemoveListener(SpawnRoad);
    }

    private void SpawnRoad() 
    {
        for (int i = 0; i < 2; i++)
        {
            if (_canIteration)
                _interation++;
            if (_interation == 15)
            {
                _lastRoad = Instantiate(_roadEnd, new Vector3(0, 0, _lastRoad.transform.position.z + 31.84f), Quaternion.Euler(-90, 0, 0));
            }
            if (_interation == 30)
            {
                _lastRoad = Instantiate(_withoutRoadEnd, new Vector3(0, 0, _lastRoad.transform.position.z + 31.84f), Quaternion.Euler(-90, 0, 0));
            }
            if (_interation < 14)
            {
                if (_lastRoad == null)
                {
                    _lastRoad = Instantiate(_clearRoad, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
                }
                else
                {
                    _lastRoad = Instantiate(_road, new Vector3(0, 0, _lastRoad.transform.position.z + 31.84f), Quaternion.Euler(-90, 0, 0));
                }
            }
            else if (_interation > 15)
            {

                if (_interation == 30)
                {
                    _interation = 0;
                }
                else
                {
                    _lastRoad = Instantiate(_withoutRoad, new Vector3(0, 0, _lastRoad.transform.position.z + 31.84f), Quaternion.Euler(-90, 0, 0));
                }
            }
        }
     
    }
}
