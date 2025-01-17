using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStarComponent : MonoBehaviour,IThunderTrigger
{
    private Transform moveTo;

    private bool isUsed;

    private void Start()
    {
        moveTo = GameManager.starMoveToPosition;
    }

    public void Use() 
    {
        if (isUsed)
            return;
        isUsed = true;
        transform.DOScale(Vector3.zero, 0.25f).
            OnComplete(() => transform.DOMove(Camera.main.ScreenToWorldPoint(moveTo.position), 1f).
                OnComplete(() => { GameManager.thunderMaxStarsEarnedCount++; Destroy(gameObject); }));
    }
}
