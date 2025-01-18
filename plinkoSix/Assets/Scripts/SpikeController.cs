using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
            return;
        if (other.TryGetComponent(out Balls ball))
        {

            ScoreCounter.score += Random.Range(4, 8);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        }
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 180 * Time.deltaTime);
    }
}
