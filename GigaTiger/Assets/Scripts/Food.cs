using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PickUpObject
{
    public delegate void DetectFoodDelegate();
    public static event DetectFoodDelegate DetectFoodEvent;

    public override void Apply()
    {
        if (DetectFoodEvent != null)
            DetectFoodEvent();


        base.Apply();
    }

}
