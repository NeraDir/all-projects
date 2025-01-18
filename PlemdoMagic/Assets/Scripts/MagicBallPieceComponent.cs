using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallPieceComponent : MonoBehaviour
{
    private float rotationSpeed;

    private bool canRitate;

    public float accuracy;

    private void OnMouseDown()
    {
        rotationSpeed = 120;
       
    }

    private void OnMouseUp() 
    {
        rotationSpeed = 0;
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        accuracy = Mathf.Abs(transform.rotation.z) * 25;
    }
}
