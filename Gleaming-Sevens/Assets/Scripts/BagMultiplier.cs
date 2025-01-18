using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BagMultiplier : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void UpdateMultiplier(int value);
    public static event UpdateMultiplier MultiplierHasBeenDetect;

    private Transform myTransform;
    private bool canFollowByInput;

    private Vector3 currentPos;

    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();
        canFollowByInput = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MultiplierItem multiplierItem))
        {
            if (MultiplierHasBeenDetect != null)
            {
                MultiplierHasBeenDetect(multiplierItem.multiplieValue);
            }
           Destroy(multiplierItem.gameObject);
        }
    }


    private void FixedUpdate()
    {
        if (canFollowByInput)
        {
            currentPos = new Vector3(Input.mousePosition.x, myTransform.position.y, myTransform.position.z);
            myTransform.position = currentPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canFollowByInput = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canFollowByInput = false;
    }
}
