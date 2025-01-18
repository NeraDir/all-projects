using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tubecontroller : MonoBehaviour
{
    [SerializeField]
    private crystallcomponents _crystallPref;

    public static UnityEvent spawnedCrystall = new UnityEvent();

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Input.mousePosition.x,transform.position.y,transform.position.z), 1000*Time.deltaTime);
        if (Input.GetMouseButtonUp(0))
        {
            crystallcomponents tempCrystall = Instantiate(_crystallPref, transform.position, Quaternion.identity,transform.parent);
            tempCrystall.transform.SetSiblingIndex(1);
            tempCrystall.SetData(gamecontrollercomponent.currentCrystall.Index, gamecontrollercomponent.currentCrystall.Sprite, gamecontrollercomponent.currentCrystall.Scale, gamecontrollercomponent.currentCrystall.Score);
            spawnedCrystall?.Invoke();
        }
    }
}
