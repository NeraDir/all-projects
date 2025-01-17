using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiliveryPointer : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetPosition;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(_targetPosition.transform.position.x, transform.position.y, _targetPosition.transform.position.z) - transform.position);

        //if(Vector3.Distance(new Vector3(_targetPosition.transform.position.x, transform.position.y, _targetPosition.transform.position.z), transform.position) < 5f)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    //public void PointerActivator()
    //{
    //    gameObject.SetActive(true);
    //}
}
