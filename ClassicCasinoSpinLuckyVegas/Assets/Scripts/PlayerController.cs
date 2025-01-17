using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Joystick _js;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _bulletSpawnPosition;

    [SerializeField]
    private GameObject _bullet;

    private Rigidbody _rb;

    private Animator Animator;

    [SerializeField]
    private GameObject _presaluneScreen;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    public void OnClickShoot() 
    {
        Instantiate(_bullet, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
    }

    private void LateUpdate()
    {
        if (!GameController.canGO)
            return;
        Animator.SetBool("GO", true);
        _rb.velocity = new Vector3(_js.Horizontal * _speed, _rb.velocity.y, _speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, _js.Horizontal * 35, transform.rotation.z), 1 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoinComponent coin))
        {
            coin.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {GameController.currentCoins +=  1 * GameController.xValue; Destroy(coin.gameObject); });
        }
        else if (other.CompareTag("Finish"))
        {
            GameController.canGO = false;
            _presaluneScreen.SetActive(true);
        }
    }
}
