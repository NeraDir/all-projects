using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell : PickUpObject
{

    public delegate void DetectDumbbellDelegate();
    public static event DetectDumbbellDelegate DetectDumbbellEvent;

    public override void Apply()
    {
        if (DetectDumbbellEvent != null)
            DetectDumbbellEvent();

        base.Apply();

    }
}
