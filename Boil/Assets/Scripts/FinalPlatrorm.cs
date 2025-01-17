using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatrorm : MonoBehaviour
{
    public FinalPlatrormStartTrigger finalPlatrormStartTrigger;
    public FinalPlatrormFinalTrigger finalPlatrormFinalTrigger;


    public delegate void BallTriggerDelegate();
    public static event BallTriggerDelegate BallOnStartFinalPlatformEvent;
    public static event BallTriggerDelegate BallOnFinalFinalPlatformEvent;



    private void Init()
    {
        finalPlatrormStartTrigger.SetParrent(this);
        finalPlatrormFinalTrigger.SetParrent(this);
    }

    private void Start()
    {
        Init();
    }



    public void CallBallOnStartFinalPlatformEvent()
    {
        if (BallOnStartFinalPlatformEvent != null)
            BallOnStartFinalPlatformEvent();
    }
    public void CallBallOnFinalFinalPlatformEvent()
    {
        if (BallOnFinalFinalPlatformEvent != null)
            BallOnFinalFinalPlatformEvent();
    }

}
