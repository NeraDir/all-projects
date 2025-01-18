using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeartComponent : MonoBehaviour
{
    [SerializeField] private GameObject _heartDestroyEffect;

    private Vector3 _beginScale;

    private void Awake()
    {
        _beginScale = transform.localScale;
    }
    
    private void OnEnable()
    {
        transform.DOScale(_beginScale, 0.25f);
    }
    
    public void DestroyMe()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            Instantiate(_heartDestroyEffect, transform.position, Quaternion.identity,transform.parent);
            gameObject.SetActive(false);
        });
    }
}
