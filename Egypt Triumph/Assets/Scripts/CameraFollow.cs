using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Transform player_position;

    private void Start()
    {
        player_position = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 target = new Vector3()
        {
            x = 0,
            y = this.player_position.position.y - 3.8f,
            z = -10,
        };

        Vector3 pos = Vector3.Lerp(this.transform.position, target, speed * Time.deltaTime);
        this.transform.position = pos;
    }
}
