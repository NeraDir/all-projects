using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : PickUpObject
{

    public override void Apply()
    {
        Debug.Log("Star");
        GamePlayController.starsCount++;
        base.Apply();
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0,1,0), 180 * Time.deltaTime);
    }
}
