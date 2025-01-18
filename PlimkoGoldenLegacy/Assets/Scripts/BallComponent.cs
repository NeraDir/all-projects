using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private Rigidbody2D ball;

    private void Start()
    {
        ball = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FallPlace place))
        {
            StartCoroutine(LetsGo(place.transform));
            GameManager.score += place.xvalue;
        }
    }

    private IEnumerator LetsGo(Transform pos) 
    {
        ball.velocity = Vector3.zero;
        ball.bodyType = RigidbodyType2D.Kinematic;
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 2 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, pos.position, 1 * Time.deltaTime);

            yield return null;
        }
        GameManager.ballsAlive.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
