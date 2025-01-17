using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloObjectsMoveComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotationDirections;

    [SerializeField]
    private float _rotationSpeed;

    private void LateUpdate()
    {
        if(BuffaloRunOwlComponent.isStop)
            return;
        transform.position += new Vector3(0, 0, -1) * 5 * Time.deltaTime;

        transform.Rotate(_rotationDirections, _rotationSpeed * Time.deltaTime);
    }
}
