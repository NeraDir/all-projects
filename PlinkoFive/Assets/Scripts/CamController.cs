using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private Transform _parentOfTargets;

    public static UnityEvent moveObjectsToViewPos = new UnityEvent();
    public static UnityEvent stopMove = new UnityEvent();


    private void Start()
    {
        moveObjectsToViewPos.AddListener(OnMoveObjects);
        stopMove.AddListener(OnStopMove);
    }

    private void OnMoveObjects()
    {
        
    }

    private void OnStopMove()
    {
        BallController._isClicked = false;
    }
}
