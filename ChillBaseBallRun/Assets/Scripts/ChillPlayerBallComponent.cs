using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillPlayerBallComponent : MonoBehaviour
{
    private Rigidbody _chillBallBody;

    [SerializeField]
    private float _chillBallSpeed;

    [SerializeField]
    private GameObject _chillStarGetEffect;

    [SerializeField]
    private LayerMask _chillGroundMask;

    private bool _isFirstClick;
    private bool _isGround;

    private float _timer;

    private bool _isSecondClick;

    private void Start()
    {
        _chillBallBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        _chillBallBody.velocity = new Vector3(_chillBallBody.velocity.x, _chillBallBody.velocity.y, _chillBallSpeed);
        _isGround = Physics.CheckSphere(transform.position, 1f, _chillGroundMask);
        if (_isGround)
        {
            _isSecondClick = false;
            if (Input.GetMouseButtonDown(0))
            {
                _chillBallBody.AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isFirstClick = true;
            }
        }
        if (_isFirstClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("isDouble");
                _chillBallBody.AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);
                _isFirstClick = false;
                _isSecondClick = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ChillStarComponent star))
        {
            star.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                Destroy(star.gameObject);
                Instantiate(_chillStarGetEffect, star.transform.position, Quaternion.identity);
            });
        }
        if (other.TryGetComponent(out ChillPlatformTrigger platform))
        {
            ChillGameController.chillSpawnPlatforms?.Invoke();
            Destroy(platform.transform.parent.gameObject, 10);
        }
        if (other.TryGetComponent(out ChillTrapComponent trap))
        {
            ChillGameController.chillBallIsDeath?.Invoke();
        }
    }
}
