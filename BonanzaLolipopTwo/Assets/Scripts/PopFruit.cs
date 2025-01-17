using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopFruit : MonoBehaviour
{
    public PopFruitsType popFruitType;

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(1, 1, 0.5f), 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PopHandler handle))
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
        }
    }
}
