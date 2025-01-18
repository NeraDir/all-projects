using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesComponent : MonoBehaviour
{
    public bool isMove;

    private void Start()
    {
        float startYPos = transform.position.y;
        if (!isMove)
            return;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y - 6, 2f));
        sequence.Append(transform.DOMoveY(startYPos, 2f));
        sequence.SetLoops(-1, LoopType.Restart);
    }
}
