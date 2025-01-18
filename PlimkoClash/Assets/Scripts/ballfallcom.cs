using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballfallcom : MonoBehaviour
{
    private bool _isTriggered;

    [SerializeField]
    private Sprite[] _ballsSprites;

    private Image _ballImage;

    private Rigidbody _ballRigidbody;

    private void Start()
    {
        _ballImage = GetComponent<Image>();
        _ballRigidbody = GetComponent<Rigidbody>();
        _ballImage.sprite = _ballsSprites[Random.Range(0,_ballsSprites.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ballfallXcom xcom))
        {
            if (_isTriggered)
                return;
            _isTriggered = true;
            _ballRigidbody.velocity = Vector3.zero;
            Destroy(_ballRigidbody);
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject); xcom.ballsIncase++; });
        }
    }
}
