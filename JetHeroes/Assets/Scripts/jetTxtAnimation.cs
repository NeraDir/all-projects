using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetTxtAnimation : MonoBehaviour
{
    private Vector3 beginPos;

    private void OnEnable()
    {
        beginPos = transform.position;
        transform.DOScale(Vector3.zero, 1.3f).OnComplete(() => SetDefault());
    }

    private void SetDefault() 
    {
        transform.position = beginPos;
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
}
