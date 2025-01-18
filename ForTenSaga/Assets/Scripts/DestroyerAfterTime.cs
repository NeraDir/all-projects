using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DestroyerAfterTime : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 3;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(()=>
        {
            Destroy(this.gameObject);
        });
    }
}
