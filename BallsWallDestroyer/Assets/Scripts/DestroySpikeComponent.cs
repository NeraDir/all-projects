using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpikeComponent : MonoBehaviour
{
    [SerializeField]
    private Collider collider;

    private Vector3 beganScale;

    private void Start()
    {
        beganScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(beganScale, 0.25f);
    }

    public void OnTrigger(Rigidbody body) 
    {
        Destroy(collider);
        body.AddForce(new Vector3(-2, 0, 0) * 10, ForceMode.Impulse);
        GameController.ballHeartsCount--;
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
    }
}
