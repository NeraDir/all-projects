using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotateTrigger : MonoBehaviour
{

    private Transform myTransform;
    [HideInInspector]
    public CircleCollider2D myCircleCollider2D;

    private Coroutine rotateCor;

    [HideInInspector]
    public bool canTriggerWithBall;
    [HideInInspector]
    public bool hasBall;

    private float defaultTriggerRadius;

    public bool isLast;
    public bool isFirst;

    private MagneticFieldAnimationController magneticFieldAnimationController;

    [HideInInspector]
    public Transform ballDetecterTriggerPoint;
    [HideInInspector]
    public Transform parentPoint;
    [HideInInspector]
    public Transform ballRespawnPoint;

    private void OnEnable()
    {
        canTriggerWithBall = true;
        hasBall = false;
        myTransform = GetComponent<Transform>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        defaultTriggerRadius = myCircleCollider2D.radius;

        magneticFieldAnimationController = transform.GetChild(1).gameObject.GetComponent<MagneticFieldAnimationController>();



        InputManager.ScreenTouchDetected += DetectShotBallEvent;

        if (isLast)
        {
            ballDetecterTriggerPoint = transform.parent.parent.GetComponent<BG_SegmentController>().ballDetecterPoint;
        }
        if (isFirst)
        {
            parentPoint = transform.parent.parent;
        }


        ballRespawnPoint = transform.GetChild(0);
    }


    private void OnDisable()
    {
        InputManager.ScreenTouchDetected -= DetectShotBallEvent;
    }

    private void FixedUpdate()
    {
        
    }



    public void DetectShotBallEvent()
    {
        if (hasBall)
        {
            hasBall = false;

            Invoke(nameof(ResetLastRotationTrigger), 0.3f);
        }
    }

    private void ResetLastRotationTrigger()
    {
        myCircleCollider2D.enabled = true;
        myCircleCollider2D.radius = defaultTriggerRadius;
        hasBall = false;

        magneticFieldAnimationController.PlayEnebleAnimation();
    }

    public void DisableCollider()
    {
        myCircleCollider2D.radius = 10;
        myCircleCollider2D.enabled = false;

        magneticFieldAnimationController.PlayDisableAnimation();
    }


}
