using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thorns : Obstacle
{
    private Sequence idleMoveSequence;
    private Transform tTransform;

    [SerializeField]
    private float maxYpos;
    [SerializeField]
    private float minYpos;


    private void OnEnable()
    {
        tTransform = GetComponent<Transform>();

        MoveThorns();
    }




    private void MoveThorns()
    {
        idleMoveSequence = DOTween.Sequence();
        idleMoveSequence.Append(tTransform.DOLocalMoveY(minYpos, 1f));
        idleMoveSequence.Append(tTransform.DOLocalMoveY(maxYpos, 0.5f));
        idleMoveSequence.SetLoops(-1, LoopType.Yoyo);
    }
}
