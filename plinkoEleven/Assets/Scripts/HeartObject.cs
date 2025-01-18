using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObject : MonoBehaviour, ICollisionObject
{
    private void Start()
    {

    }

    public void Use()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            Destroy(gameObject);
            PlayerController.PlayerGetHeart?.Invoke();
        });
    }
}
