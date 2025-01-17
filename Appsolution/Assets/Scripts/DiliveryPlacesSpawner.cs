using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiliveryPlacesSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _diliveryObject;
    [SerializeField]
    private DiliveryPointer _diliveryPointer;
    [SerializeField]
    private GameObject[] _diliveryPlaces;

    private Vector3 _lastDiliveryPosition = Vector3.zero;
    private int randomPlaceIndex;

    private void Start()
    {
        SetDiliveryPlace();
    }

    public void SetDiliveryPlace()
    {
        randomPlaceIndex = Random.Range(0, _diliveryPlaces.Length);

        if(_diliveryPlaces[randomPlaceIndex].transform.position != _lastDiliveryPosition)
        {
            SpawnDiliveryPlace();
            //_diliveryPointer.PointerActivator();
        }
        else
        {
            SetDiliveryPlace();
        }
    }

    public void SpawnDiliveryPlace()
    {
        _diliveryObject.transform.position = _diliveryPlaces[randomPlaceIndex].transform.position;
        _diliveryObject.transform.rotation = _diliveryPlaces[randomPlaceIndex].transform.rotation;

        _diliveryObject.SetActive(true);

        _lastDiliveryPosition = _diliveryObject.transform.position;
    }
}
