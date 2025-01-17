using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillMovableTrapComponent : MonoBehaviour
{
    private void Start()
    {
        Sequence dotweeSequence = DOTween.Sequence();
        if (Random.Range(0,2) != 0)
        {
            transform.position = new Vector3(-1.633f, transform.position.y, transform.position.z);
            dotweeSequence.Append(transform.DOMoveX(1.633f, 2));
            dotweeSequence.Append(transform.DOMoveX(-1.633f, 2));
        }
        else
        {
            transform.position = new Vector3(1.633f, transform.position.y, transform.position.z);
            dotweeSequence.Append(transform.DOMoveX(-1.633f, 2));
            dotweeSequence.Append(transform.DOMoveX(1.633f, 2));
        }
        dotweeSequence.SetLoops(-1, LoopType.Yoyo);
    }
}
