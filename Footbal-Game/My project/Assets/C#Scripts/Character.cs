using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rb2D;
    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(ScreenPartButton.x * _speed, _rb2D.velocity.y);
       /* float x = _rb2D.position.x + ScreenPartButton.x * Time.fixedDeltaTime * _speed;
        if (Input.GetAxis("Horizontal") != 0)
        {
            x = _rb2D.position.x + Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed;
        }
        _rb2D.position = new Vector2(x, _rb2D.position.y);*/
    }
}
