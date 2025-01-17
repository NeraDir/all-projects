using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLookPointController : MonoBehaviour
{
    private Transform _myTransform;
    private Transform _boatTranform;

    private Vector3 _myPos;
    private float _zOffcet;

    private float _currentXpos;
    private float _currentXposLerp;
    private float _distanceBetweenLines;

    private LineState _currentLineState;

    private float _switchLineSpeed;

    private bool _isInit;


    private void OnEnable()
    {

    }

    public void Init(Transform boatTransform, float distanceBetweenLines, float switchLineSpeed)
    {
        _myTransform = GetComponent<Transform>();
        _distanceBetweenLines = distanceBetweenLines;
        _boatTranform = boatTransform;
        _zOffcet = _myTransform.position.z - _boatTranform.position.z;
        _currentLineState = LineState.midle;
        _currentXpos = 0;
        _currentXposLerp = 0;
        _switchLineSpeed = switchLineSpeed;
        _isInit = true;
    }


    private void FixedUpdate()
    {
        if (_isInit)
        {
            
            _currentXposLerp = Mathf.Lerp(_currentXposLerp, _currentXpos, _switchLineSpeed);

            _myPos = new Vector3(_currentXposLerp, _boatTranform.position.y, _boatTranform.position.z + _zOffcet);
            _myTransform.position = _myPos;
        }
    }

    public void SwitchLine(LineState line, float currentXpos)
    {
        if (line == _currentLineState)
            return;

        _currentXpos = currentXpos;
        _currentLineState = line;
            
    }
}
