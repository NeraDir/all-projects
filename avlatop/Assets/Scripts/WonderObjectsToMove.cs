using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WonderObjectsToMove : MonoBehaviour
{
    public bool _isZero;

    public bool isRotateble;

    public bool isSizeble;

    private Vector3 scale;

    private Sequence _sequence;

    private void Start()
    {
        transform.position = _isZero == true ? new Vector3(0,transform.position.y,transform.position.z) : transform.position;
        if (isSizeble)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(transform.localScale / 1.1f, 0.25f));
            _sequence.Append(transform.DOScale(transform.localScale * 1.1f, 0.25f));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            if (!isRotateble)
                return;
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(transform.localScale / 1.1f, 0.25f));
            _sequence.Append(transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x, 360, transform.rotation.z), 0.25f));
            _sequence.Append(transform.DOScale(transform.localScale * 1.1f, 0.25f));
            _sequence.Append(transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z), 0.25f));
            _sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * 0.5f * Time.deltaTime;
    }
}
