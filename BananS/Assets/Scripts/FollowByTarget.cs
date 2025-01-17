using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowByTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private Vector3 mPosition;
    private Vector3 targetPos;
    public float lerpSpeed;


    private void OnEnable()
    {
        HeadSweetie.AddPartEvent += ChangeZoffset;
    }
    private void OnDisable()
    {
        HeadSweetie.AddPartEvent -= ChangeZoffset;
    }

    
    private void FixedUpdate()
    {
        targetPos = target.position;
        mPosition = new Vector3(targetPos.x + offset.x, targetPos.y + offset.y, targetPos.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, mPosition, lerpSpeed);
    }
    
    /*
    private void LateUpdate()
    {
        targetPos = target.position;
        mPosition = new Vector3(targetPos.x + offset.x, targetPos.y + offset.y, targetPos.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, mPosition, lerpSpeed);

    }
    */


    private void ChangeZoffset()
    {
        offset.z += -1.2f;
        offset.x += 0.5f;
    }
}
