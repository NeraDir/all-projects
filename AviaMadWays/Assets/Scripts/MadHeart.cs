using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadHeart : MonoBehaviour
{
    private Vector3 beginScale;
    Sequence sequence;
    private void Start()
    {
        beginScale = transform.localScale;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(beginScale * 1.15f, 0.5f));
        sequence.Append(transform.DOScale(beginScale, 0.5f));

        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public void DestroyMe() 
    {
        sequence.Kill();
        transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(gameObject));
    }
}
