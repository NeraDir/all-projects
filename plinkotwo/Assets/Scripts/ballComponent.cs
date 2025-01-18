using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ballComponent : MonoBehaviour
{
    private Rigidbody _ballBody;

    private float _speed;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private GameObject _starGetEffect;

    private bool _onGround;

    public static UnityEvent ballIsDeath = new UnityEvent();

    private bool _ballIsDead;

    private void Start()
    {
        _ballBody = GetComponent<Rigidbody>();
        swipeInput.jumpAndDown.AddListener(Jump);
    }

    private void LateUpdate()
    {
        if (_ballIsDead)
            return;
        if (Input.GetMouseButton(0))
        {
            _speed = 6;
        }
        else
        {
            _speed = 0;
        }
        _ballBody.velocity = new Vector3(_speed, _ballBody.velocity.y, _ballBody.velocity.z);
        _onGround = Physics.CheckSphere(transform.position, 2f, _groundLayer);
    }

    private void Jump(float value) 
    {
        if (!_onGround)
            return;
        _ballBody.AddForce(new Vector3(0, value, 0) * 8, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out roadTriggerComponent roadTrigger))
        {
            gameManager.roadSpawn?.Invoke();
            StartCoroutine(DestroyObject(roadTrigger.gameObject));
        }
        if (other.TryGetComponent(out starCompponent star))
        {
            star.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {Destroy(star.gameObject);gameManager.starsCount++;Instantiate(_starGetEffect, star.transform.position, Quaternion.identity); });
        }
        if (other.TryGetComponent(out spikesComponent spike))
        {
            ballIsDeath?.Invoke();
            _ballIsDead = true;
        }
        if (other.TryGetComponent(out loosComponent loos))
        {
            ballIsDeath?.Invoke();
            _ballIsDead = true;
        }
    }

    private IEnumerator DestroyObject(GameObject objecte)
    {
        yield return new WaitForSeconds(30);
        objecte.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(objecte.transform.parent.gameObject));
    }
}
