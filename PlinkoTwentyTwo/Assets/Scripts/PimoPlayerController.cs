using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoPlayerController : MonoBehaviour
{
    [SerializeField] private Joystick _js;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private AnimationCurve _abimCurve;

    private Rigidbody _ballBody;

    private void Start()
    {
        _ballBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.position += (transform.right * _js.Horizontal) * 4 * Time.deltaTime;

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit = new RaycastHit();
        Quaternion rotationRef = Quaternion.Euler(0f, 0f, 0f);
        if (Physics.Raycast(ray,out hit, _groundLayer)){
            transform.position += -transform.up * 8 * Time.deltaTime;
            rotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal.normalized), 10 * Time.deltaTime);
            transform.rotation = rotationRef;
            transform.position += transform.forward * 10 * Time.deltaTime;
        }
    }
}
