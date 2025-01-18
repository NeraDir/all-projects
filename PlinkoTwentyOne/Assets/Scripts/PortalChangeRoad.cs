using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChangeRoad : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _parts;

    [SerializeField]
    private GameObject[] _rings;

    [SerializeField]
    private GameObject[] _flags;

    public bool isLast;

    private Vector3[] _partsPositions = new Vector3[2];

    private bool _canClick;
    private bool _backRoate;

    private void Start()
    {

        foreach (var item in _rings)
        {
            item.SetActive(false);
        }
        if(Random.Range(0, 2) != 0)
        {
            _parts[Random.Range(0, _parts.Length)].SetActive(false);
        }
        for (int i = 0; i < _parts.Length; i++)
        {
            _partsPositions[i] = _partsPositions[i];
        }
        _rings[Random.Range(0, _rings.Length)].SetActive(true);
        if (isLast)
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                if (_parts[i].activeInHierarchy)
                {
                    _flags[i].SetActive(true);
                }
            }
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
        }
    }

    private void OnMouseDown()
    {
        if (_canClick)
            return;
        _canClick = true;
        if (!_backRoate)
        {
            transform.DORotateQuaternion(Quaternion.Euler(0, 0, 180), 0.5f).OnComplete(() => { _backRoate = true; _canClick = false; });
        }
        else
        {
            transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f).OnComplete(() => { _backRoate = false; _canClick = false; });
        }
    }
}
