using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : Obstacle
{


    private void OnEnable()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        //transform.position = transform.parent.position
        transform.position = new Vector3(transform.parent.position.x + (Random.Range(0,101) > 50? 40: -40)
            ,transform.position.y, transform.position.z);
    }
}
