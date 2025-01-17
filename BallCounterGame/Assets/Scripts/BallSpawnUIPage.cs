using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnUIPage : MonoBehaviour
{
    public delegate void BallSpawnPageAnimationCompleteDelegate();
    public static event BallSpawnPageAnimationCompleteDelegate BallSpawnPageAnimationCompleteEvent;



    public void CallAnimationCompleteEvent()
    {
        if (BallSpawnPageAnimationCompleteEvent != null)
            BallSpawnPageAnimationCompleteEvent();
    }
}
