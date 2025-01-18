using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartComponent : MonoBehaviour, ICollisionObject
{
    private void Start()
    {
        if (Random.Range(0, 2) != 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Use()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject); PlayerController.PlayerGetHeart?.Invoke(); });
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 90 * Time.deltaTime);
    }
}
