using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkPlaneRecoveringStatusDuisplayer : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1).OnComplete(() => Invoke(nameof(DoZero), 1));
        
    }

    private void DoZero() 
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
