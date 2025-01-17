using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveUP : MonoBehaviour
{
    private float speed = 2f; // Скорость движения вверх.

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
