using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopHandler : MonoBehaviour
{
    private bool _popHandleRotated;

    private void OnMouseDown()
    {
        if (_popHandleRotated)
            return;
        _popHandleRotated = true;
        transform.DOLocalRotateQuaternion(Quaternion.Euler(-90f, 0, -30f), 0.25f).OnComplete(() => transform.DOLocalRotateQuaternion(Quaternion.Euler(-90, 0, 90f), 0.25f).OnComplete(() => _popHandleRotated = false));
    }
}
