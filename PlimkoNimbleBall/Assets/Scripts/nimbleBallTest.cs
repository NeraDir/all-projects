using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleBallTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out nimbleTriggerPlace place))
        {
            Rigidbody nody = GetComponent<Rigidbody>();
            nody.velocity = Vector3.zero;
            Destroy(nody);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { nimbleGameManager.currentScore += place.nimbleScore;Destroy(gameObject); });
        }
    }
}
