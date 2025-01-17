using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sawmill : Obstacle
{
    private Sequence idleMoveSequence;
    private Transform sTransform;

    [SerializeField]
    private float maxZpos;
    [SerializeField]
    private float minZpos;


    [SerializeField]
    private Vector3 direction;


    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform sawObject;


    private void OnEnable()
    {
        sTransform = GetComponent<Transform>();

        MoveSawmill();
    }

    private void FixedUpdate()
    {
        sawObject.Rotate(0, 0, 10);
    }

    private void MoveSawmill()
    {
        idleMoveSequence = DOTween.Sequence();

        if (direction.z != 0)
        {
            idleMoveSequence.Append(sTransform.DOLocalMoveZ(maxZpos, moveSpeed / 2));
            idleMoveSequence.Append(sTransform.DOLocalMoveZ(minZpos, moveSpeed / 2));
        }
        else if (direction.x != 0)
        {

        }


        idleMoveSequence.SetLoops(-1, LoopType.Yoyo);
    }
}
