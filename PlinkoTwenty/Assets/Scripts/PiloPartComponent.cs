using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiloPartComponent : MonoBehaviour, ITakebleComponent
{
    [SerializeField]
    private PiloCarComponent[] _carComponents;

    public static int partsReachedCount;

    private void Start()
    {
        foreach (var car in _carComponents)
        {
            car.carType = CarType.Ride;
        }
        if (partsReachedCount > 10)
        {
            if (Random.Range(0, 2) != 0)
            {
                _carComponents[Random.Range(0, _carComponents.Length)].carType = CarType.FastRide;
            }
        }
        if (partsReachedCount > 20)
        {
            if (Random.Range(0, 2) != 0)
            {
                _carComponents[Random.Range(0, _carComponents.Length)].carType = CarType.ReverseRide;
            }
        }
        if (Random.Range(0,2) != 0)
        {
            _carComponents[Random.Range(0, _carComponents.Length)].carType = CarType.idle;
        }
        if (Random.Range(0,2) != 0)
        {
            _carComponents[Random.Range(0, _carComponents.Length)].gameObject.SetActive(false);
        }
        foreach (var car in _carComponents)
        {
            car.Init();
        }
    }

    public void OnTake()
    {
        partsReachedCount++;
        PiloGameManager.partReachead?.Invoke();
    }
}
