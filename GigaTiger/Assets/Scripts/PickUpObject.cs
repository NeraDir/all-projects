using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class PickUpObject : MonoBehaviour
{
    private Sequence mSequence;
    private Transform model;

    private void OnEnable()
    {
        model = transform.GetChild(0);

        mSequence = DOTween.Sequence();
        mSequence.Append(model.DOLocalMoveY(model.position.y + 4f, 1.2f));
        mSequence.Append(model.DOLocalMoveY(model.position.y, 1.2f));
        mSequence.SetLoops(-1, LoopType.Yoyo);

    }

    public virtual void Apply()
    {
        DestroyModel();
    }

    private void DestroyModel()
    {
        mSequence.Kill();
        transform.DOScale(new Vector3(transform.localScale.x + 1.1f, transform.localScale.y + 1.1f, transform.localScale.z + 1.1f), 0.25f).
            OnComplete(() => transform.DOScale(Vector3.zero, 0.5f)).OnComplete(() => DestroyMe());
            
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

}
