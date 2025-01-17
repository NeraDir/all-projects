using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FruitKnifeComponent : MonoBehaviour
{
    private bool _knifeMove;
    
    private Rigidbody2D _rigidbody;

    public static bool _knifeActive;

    public static UnityEvent knifeRespawn = new UnityEvent();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
         _knifeActive = true;
    }

    private void LateUpdate()
    {
        if (!_knifeActive)
            return;
        if (Input.GetMouseButtonDown(0) && FruitMainGameManager.knifesCountToLevel > 0)
        {
            if (_knifeMove)
                return;
            _knifeMove = true;
            FruitMainGameManager.knifesCountToLevel -= 1;
        }

        if (_knifeMove)
        {
            transform.position += Vector3.up * 10000 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out FruitMainCmponent mainFruit))
        {
            transform.parent = mainFruit.transform;
            transform.SetSiblingIndex(0);
            _knifeActive = false;
            _knifeMove = false;
            if (FruitMainGameManager.knifesCountToLevel > 0)
            {
                knifeRespawn?.Invoke();
            }
            Destroy(this);
        }
        if (other.TryGetComponent(out FruitComponent fruit))
        {
            fruit.Use();
        }
        if (other.CompareTag("knife") && _knifeActive && _knifeMove)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _knifeActive = false;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
            _rigidbody.AddForce(new Vector2(Random.Range(-1, 1), -1) * 2000, ForceMode2D.Impulse);
            StartCoroutine(Destroer());
        }
    }

    private IEnumerator Destroer()
    {
        yield return new WaitForSeconds(1.5f);
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 10 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
        if (FruitMainGameManager.knifesCountToLevel > 0)
        {
            knifeRespawn?.Invoke();
        }
    }
}
