using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillBgObject : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _rotations;

    private bool _isMoveble;
    private bool _isTop;

    private int _rotationIndex;

    private void Start()
    {
        _isMoveble = Random.Range(0,2) != 0 ? true:false;
        if( _isMoveble)
        {
            Sequence sequence = DOTween.Sequence();
            _isTop = Random.Range(0,2) != 0 ? true:false;
            if (_isTop)
            {
                transform.position = new Vector3(transform.position.x, 3.68f, transform.position.z);
                sequence.Append(transform.DOMoveY(-3.68f, 2));
                sequence.Append(transform.DOMoveY(3.68f, 2));
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -3.68f, transform.position.z);
                sequence.Append(transform.DOMoveY(3.68f, 2));
                sequence.Append(transform.DOMoveY(-3.68f, 2));
            }
            sequence.SetLoops(-1, LoopType.Yoyo);
        }
        _rotationIndex = Random.Range(0,_rotations.Length);
    }

    private void LateUpdate()
    {
        transform.Rotate(_rotations[_rotationIndex], 180 * Time.deltaTime);
    }
}
