using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusBall : MonoBehaviour
{
    private Rigidbody _ballBody;

    private void Start()
    {
        _ballBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out bonusBallPlaceComponent bonusPlace))
        {
            _ballBody.velocity = Vector3.zero;
            Destroy(_ballBody);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject); bonusGameManager.starsCount += bonusPlace.starsX; });
        }
    }
}
