using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    private void LateUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
