using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDefenceText : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 1, 0.25f).OnComplete(() => transform.DOScale(Vector3.zero,0.1f).OnComplete(() => Destroy(gameObject)));
    }
}
