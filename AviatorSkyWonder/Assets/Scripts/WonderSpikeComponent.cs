using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderSpikeComponent : MonoBehaviour, ICollisionableComponent
{
    public void Use(Transform target)
    {
        GameManager.wonderPlaneControllerComponent.GetDamage();
        transform.DOScale(transform.localScale * 2, 0.25f).OnComplete(() => transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => Destroy(gameObject)));
    }
}
