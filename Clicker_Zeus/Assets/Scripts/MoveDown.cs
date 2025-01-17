using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float speed = 1f; // Скорость движения вверх.

    void Update()
    {
        // Перемещаем объект вверх по оси Y.
        transform.Translate(-Vector2.up * speed * Time.deltaTime);
    }
}
