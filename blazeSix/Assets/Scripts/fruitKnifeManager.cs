using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitKnifeManager : MonoBehaviour
{
    private Transform _target;

    private Rigidbody2D _MyBody;

    private bool _isTriggered;
    
    private void Start()
    {
        _MyBody = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Target").transform;
    }

    private void LateUpdate()
    {
        Vector3 direction = _target.position - transform.position;
        transform.position += direction * 0.75f * Time.deltaTime;
        direction.Normalize();
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out fruitShieldManager shield))
        {
            if (_isTriggered)
                return;
            if (shield.GetShieldState())
            {
                _isTriggered = true;
                _MyBody.bodyType = RigidbodyType2D.Dynamic;
                transform.DOScale(Vector3.zero, 5).OnComplete(() => Destroy(gameObject));
            }
            else
            {
                _isTriggered = true;
                gameController.GetDamage?.Invoke();
                transform.DOScale(Vector3.zero, .5f).OnComplete(() => Destroy(gameObject));
            }
        }
    }
}
