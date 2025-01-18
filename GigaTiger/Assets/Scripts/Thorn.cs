using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thorn : Obstacle
{
    private Sequence move;

    private void Start()
    {
        move = DOTween.Sequence();
        move.Append(transform.DOLocalMoveY(7.7f, 0.5f));
        move.Append(transform.DOLocalMoveY(-9, 1));
        move.SetLoops(-1, LoopType.Yoyo);
    }

}
