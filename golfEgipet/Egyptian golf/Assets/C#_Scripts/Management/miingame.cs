using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miingame : MonoBehaviour
{
    [SerializeField]
    private Transform _ball;

    [SerializeField]
    private float Width;

    [SerializeField] 
    private float Height;

    private void Start()
    {
        _ball.position = new Vector2(Random.Range(_ball.position.x,_ball.position.x + Width),Random.Range(_ball.position.y,_ball.position.y + Height));
    }
}
