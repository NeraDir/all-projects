using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public bool isEnemie;

    public GameObject coin;


    [SerializeField]
    private GameObject _deathEffect;

    private void Start()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 70, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemieController enemie))
        {
            if (!isEnemie)
            {
                GameController.currentCoins += GameController.xValue * 1 * GameController.currentLevel;
                Instantiate(_deathEffect, transform.position, transform.rotation);
                enemie.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { Destroy(enemie.gameObject);   });
                Destroy(gameObject);
            }
        }
        if (other.TryGetComponent(out PlayerController player))
        {
            if (isEnemie) 
            {
                GameController.playerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
