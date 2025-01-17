using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chillScoreTxtComponent : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 2, 0.25f).OnComplete(() => { transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject)); });
    }
}
