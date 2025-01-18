using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeComponent : MonoBehaviour
{
    public bool canCut;

    public bool canCLick;

    private bool triggered;

    private void OnMouseDown()
    {
        if (canCLick)
            return;
        if (canCut)
            return;
        canCLick = true;
        canCut = true;
        Slice();
    }

    public void Slice() 
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotateQuaternion(Quaternion.Euler(-219.509f, -215.548f, 173.25f), 0.25f).OnComplete(() => canCut = false));
        sequence.Append(transform.DORotateQuaternion(Quaternion.Euler(-119.52f, -201.988f, 169.395f), 0.25f).OnComplete(() => { canCLick = false; triggered = false; }));

        sequence.SetLoops(1,LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LinePieceComponente linePiece))
        {
            if (!canCut)
                return;
            if (triggered)
                return;
            triggered = true;
            linePiece.Cutted();
        }
    }
}
