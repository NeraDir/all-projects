using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed;
    public int biasSpeed = 0;
    private void FixedUpdate()
    {
        if (transform.position.x <= -1.5 && biasSpeed < 0 || transform.position.x >= 1.5 && biasSpeed > 0)
            return;
            Vector3 pose = new Vector3(transform.position.x + biasSpeed, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, pose, Time.deltaTime * _speed);
        
        
    }
}
