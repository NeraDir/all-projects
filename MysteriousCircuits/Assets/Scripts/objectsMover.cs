using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsMover : MonoBehaviour
{
    private float speed;

    public bool isMover;

    private void Start()
    {
        speed = 2.5f;
        if (isMover)
            return;
        for (int i = 0; i < (gameController.LevelIndex + 1); i++)
        {
            if (i != 0)
                speed += 2.5f / i;
        }
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
    }
}
