using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour, ICollisionObject
{
    public void Use()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        GameManager.PlayerGetStar?.Invoke(transform);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 360 * Time.deltaTime);
    }
}
