using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoopMenuMove : MonoBehaviour
{
    private Sequence sequence;


    private void OnEnable()
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(30.04f, 6f));
        sequence.Append(transform.DOMoveY(26.8f, 6f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}
