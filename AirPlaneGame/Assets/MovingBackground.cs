using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private GameObject _first; 
    [SerializeField] private GameObject _second; 

    void Start()
    {
        _speed = _speed+(PlayerPrefs.GetInt("speed",0)+1)/5;
    }

    void Update()
    {
        _first.transform.position  += new Vector3(0, -_speed * Time.deltaTime, 0);
        _second.transform.position += new Vector3(0, -_speed * Time.deltaTime, 0);
        if (_first.transform.position.y <= -48) 
        {
            _first.transform.localPosition = new Vector3(0, 48, 0) + new Vector3(0, -_speed * Time.deltaTime, 0);
            var zatic = _second;
            _second = _first;
            _first = zatic;
        }
    }
}
