using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROADPARTCOMPONENT : MonoBehaviour
{
    [SerializeField] private Transform[] _parts;
    [SerializeField] private GameObject[] _roadType;
    [SerializeField] private GameObject _cannon;
    [SerializeField] private GameObject _wheel;

    private void Start()
    {
        if (Random.Range(0,2) != 0)
        {
            _roadType[1].SetActive(true);
            foreach (var part in _parts)
            {
                if (Random.Range(0, 2) != 0)
                {
                    part.GetChild(Random.Range(0, part.childCount)).gameObject.SetActive(true);
                }
            }
            if (Random.Range(0, 2) != 0)
            {
                _wheel.SetActive(true);
            }
        }
        else
        {
            _roadType[0].SetActive(true);
        }

        if (Random.Range(0,2) != 0)
        {
            _cannon.SetActive(true);
        }
    }
}
