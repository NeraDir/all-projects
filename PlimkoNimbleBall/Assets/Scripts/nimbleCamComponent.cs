using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleCamComponent : MonoBehaviour
{
    public Transform _target;

    public Vector3 _offest;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Quaternion _finishRotation;

    public bool isFinish;

    private void LateUpdate()
    {
        if (isFinish)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _finishRotation,10 * Time.deltaTime);
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, _target.position.y + _offest.y, _target.position.z + _offest.z), _speed * Time.deltaTime);
    }
}
