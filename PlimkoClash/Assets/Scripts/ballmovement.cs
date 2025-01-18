using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ballmovement : MonoBehaviour
{
    private Rigidbody _ballBody;

    private bool _isOnGround;

    private Image _ballImage;

    [SerializeField]
    private Sprite[] _ballSprites;

    [SerializeField]
    private float _ballJumpStrenght;

    [SerializeField]
    private LayerMask _groundLayer;

    public static int ballSpriteIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("ballselectedIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("ballselectedIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballselectedIndexSaveKey", value);
        }
    }

    public static UnityEvent ballIsDestroyed = new UnityEvent();

    private void Start()
    {
        _ballImage = GetComponent<Image>();
        _ballImage.sprite = _ballSprites[ballSpriteIndex];
        _ballBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && _isOnGround)
        {
            _ballBody.AddForce(Vector3.up * _ballJumpStrenght, ForceMode.Impulse);
        }
        transform.Rotate(new Vector3(0, 0, -1), 360 * Time.deltaTime);
        _isOnGround = Physics.CheckSphere(transform.position, 0.35f, _groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out starcomponent star))
        {
            star.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(star.gameObject);gamecontoller.starsCount++; });
        }
        if (other.TryGetComponent(out getball ball))
        {
            ball.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(ball.gameObject); gamecontoller.ballsCount++; });
        }
        if (other.TryGetComponent(out loosecom loose))
        {
            ballIsDestroyed?.Invoke();
        }
    }
}
