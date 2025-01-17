using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderPlaneController : MonoBehaviour
{
    public Transform[] planeFlyEffect;

    public Slider arrowRotationDisplay;

    private Quaternion lastRotation;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * GameManager.moveSpeed * Time.deltaTime;
#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -Input.GetAxis("Horizontal") * 90), 2 * Time.deltaTime);
            lastRotation = transform.rotation;
        }
        else
        {
            transform.rotation = lastRotation;
        }
#endif
        if (Input.acceleration.x != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -Input.acceleration.x * 90), 2 * Time.deltaTime);
            lastRotation = transform.rotation;
        }
        else
        {
            transform.rotation = lastRotation;
        }
        arrowRotationDisplay.value = -transform.rotation.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IThunderTrigger trigger)) 
        {
            if(!GameManager.isEnd)
                trigger.Use();
        }
    }
}
