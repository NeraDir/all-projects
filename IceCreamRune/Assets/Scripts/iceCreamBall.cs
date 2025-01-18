using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class iceCreamBall : MonoBehaviour
{
    private const float _deadzone = 10;

    private Vector2 _swipeDelta, _startTouch;

    private bool _isSwiping;

    private bool _isGround;

    private Rigidbody2D _iceBody;

    [SerializeField]
    private AudioSource _iceSource;

    [SerializeField]
    private AudioClip _iceClip;

    [SerializeField]
    private LayerMask _layerMask;

    private bool _iceMove;

    private void Start()
    {
        _iceBody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (_iceMove)
            return;
        _iceBody.velocity = new Vector2(5, _iceBody.velocity.y);

        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _isSwiping = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _startTouch = _swipeDelta = Vector2.zero;
        }

        _isGround = Physics2D.OverlapCircle(transform.position, 1, _layerMask);

        if (_isSwiping)
        {
            _swipeDelta = Vector2.zero;
            if (_startTouch != Vector2.zero)
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
            if (_swipeDelta.magnitude > _deadzone)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;

                if (Mathf.Abs(x) < Mathf.Abs(y))
                {
                    if (y < 0)
                    {
                            _iceBody.AddForce(-Vector2.up * 4, ForceMode2D.Impulse);
                    }
                    else 
                    {
                        if(_isGround)
                            _iceBody.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
                    }
                }

                _startTouch = _swipeDelta = Vector2.zero;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IceCreamRoadTrigger ball))
        {
            IceCreamGameManager.balltriggeredRoad?.Invoke();
            _iceBody.velocity = Vector2.zero;
            _iceMove = ball.End();
        }
        if (other.TryGetComponent( out IceCreamStars star))
        {
            star.transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => { _iceSource.PlayOneShot(_iceClip); Destroy(star.gameObject);IceCreamGameManager.iceCreamStarsCount++; });
        }
        if (other.TryGetComponent(out IceCreamSpikes spike))
        {
            transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => { IceCreamGameManager.ballIsDeath?.Invoke(); });
        }
        if (other.TryGetComponent(out iceCreamGetBall getBall))
        {
            getBall.transform.DOScale(Vector2.zero, 0.25f).OnComplete(() =>
            {
                Destroy(getBall.gameObject);
                IceCreamContainer.iceCreamContainerUpdate?.Invoke(getBall.index);
            });
        }
    }
}
