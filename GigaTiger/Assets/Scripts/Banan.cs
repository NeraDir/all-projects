using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banan : Obstacle
{
    private Sequence sequence;


    private void Start()
    {

        Invoke(nameof(SetLoopSize), 1.1f);

    }

    public void SetLoopSize()
    {
        Vector3 defSize = transform.localScale;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(defSize * 1.1f, 2f));
        sequence.Append(transform.DOScale(defSize, 2f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TigerEntityColliderManager tiger))
        {
            sequence.Kill();
            transform.DOMove(transform.position + new Vector3(-100, 0, -100), 0.5f);
        }
    }


    private void Move()
    {

    }
}
