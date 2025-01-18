using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicGlideBallManager : MonoBehaviour
{
    private MeshRenderer _magiclGlideBallRenderer;

    [SerializeField] private Material[] _magicGlideBallSkins;
    [SerializeField] private LayerMask _magicGlideGroundLayer;
    [SerializeField] private GameObject _magicGlideStarGetEffect;

    private Rigidbody _magicGlideRigidbody;

    public static UnityEvent MagicGlideBallDeath = new UnityEvent();

    private float _magicGlideForwardSpeed;

    private float _magicGlideRightSpeed;

    private bool _magicGlideOnGround;

    private void Start()
    {
        _magicGlideRigidbody = GetComponent<Rigidbody>();
        _magiclGlideBallRenderer = GetComponent<MeshRenderer>();
        _magiclGlideBallRenderer.material = _magicGlideBallSkins[MagicGlideGameManager.MagicGlideSkinIndex];
    }

    private void LateUpdate()
    {
        _magicGlideOnGround = Physics.CheckSphere(transform.position, 2, _magicGlideGroundLayer);
        _magicGlideRigidbody.velocity = new Vector3(Input.acceleration.x * 8, _magicGlideRigidbody.velocity.y, Input.acceleration.y * 8);
    }

    public void OnMoveForward(int increment)
    {
        _magicGlideForwardSpeed = increment;
    }

    public void OnMoveForwardStop()
    {
        _magicGlideForwardSpeed = 0;
    }

    public void OnMoveRight(int increment)
    {
        _magicGlideRightSpeed = increment;
    }

    public void OnMoveRightStop()
    {
        _magicGlideRightSpeed = 0;
    }

    public void OnJump()
    {
        if (!_magicGlideOnGround)
            return;
        _magicGlideRigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MagicGlideStarManager star))
        {
            Instantiate(_magicGlideStarGetEffect, star.transform.position, Quaternion.identity);
            MagicGlideGameManager._magicGladeStars += 1;
            Destroy(star.gameObject);
        }
        if (other.TryGetComponent(out MagicGladeTrapManager trap))
        {
            MagicGlideBallDeath?.Invoke();
        }
        if (other.TryGetComponent(out MagicGladePlatformTriggerManager trigger))
        {
            MagicGlideGameManager.MagicGladePlatformReached?.Invoke();
        }
        if (other.TryGetComponent(out MagicGlideDeathManager death))
        {
            MagicGlideBallDeath?.Invoke();
        }
    }
}
