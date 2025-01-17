using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private MainMenager mainMenager;
    private Rigidbody rigidbody;

    private void Start()
    {
        mainMenager = MainMenager.instance;
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(0,0,-1) * mainMenager.speed * Time.deltaTime;
    }
    }
