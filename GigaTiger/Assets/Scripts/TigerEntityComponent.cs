using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TigerEntityComponent : MonoBehaviour
{
    public float stepSizeValue;
    public float changeLineSpeed;

    public float moveForwardSpeed;

    public float jumpPower;



    private float startXpos;

    private TigerEntityAnimationManager animationManager;
    private TigerEntityHealth tigerEntityHealth;

    private Rigidbody rb;

    private bool canMoveForward = false;

    public LayerMask groundMask;
    public float checkGroundRadius;
    private bool hasGround;


    private Coroutine waitGroundCoroutine;

    private Vector3 lastPreJumpPos;

    private bool canInputHadle;


    public void Init()
    {
        canInputHadle = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        animationManager = GetComponent<TigerEntityAnimationManager>();
        tigerEntityHealth = GetComponent<TigerEntityHealth>();
    }


    private void OnEnable()
    {
        InputSwipeHandler.SwipeCompleteEvent += InputSwipeHandle;
        TigerEntityColliderManager.ObstacleTriggerEvent += ObstacleTriggerHandler;
        Dumbbell.DetectDumbbellEvent += AddSize;
        TigerEntityColliderManager.FinalTriggerEvent += StartFinalWalk;
    }
    private void OnDisable()
    {
        InputSwipeHandler.SwipeCompleteEvent -= InputSwipeHandle;
        TigerEntityColliderManager.ObstacleTriggerEvent -= ObstacleTriggerHandler;
        Dumbbell.DetectDumbbellEvent -= AddSize;
        TigerEntityColliderManager.FinalTriggerEvent -= StartFinalWalk;
    }


    private void Start()
    {
        Init();
    }

    public void ContinueMove()
    {
        canInputHadle = true;
        canMoveForward = true;
        rb.isKinematic = false;
        animationManager.ChangeToRunAnimation();
    }


    private void Update()
    {
        hasGround = Physics.CheckSphere(transform.position,checkGroundRadius, groundMask);

        if (hasGround)
        {
            lastPreJumpPos = Vector3.zero;
        }
        //Debug.Log(hasGround);
    }

    private void FixedUpdate()
    {
        if (canMoveForward)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveForwardSpeed);
        }

        
    }


    public void StartFinalWalk()
    {
        canInputHadle = false;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        //rb.constraints = 2;
        moveForwardSpeed /= 2;
        animationManager.ChangeToWalkAnimation();
    }


    public void InputSwipeHandle(Vector3 inputDirection)
    {
        if (!canInputHadle)
            return;

        //Debug.Log("input direction: " + inputDirection);

        if (inputDirection.x != 0)
        {
            ChangeLine(inputDirection.x);
        }
        else if(inputDirection.y != 0)
        {
            if(inputDirection.y > 0)
            {
                Jump();
            }
            else
            {
                Sliding();
            }
        }
    }


    private void Jump()
    {
        if (!hasGround)
            return;

        lastPreJumpPos = transform.position;
        //StopMove();
        rb.isKinematic = false;

        //rb.AddForce(new Vector3(0, 1f, 1f) * jumpPower, ForceMode.Impulse);

        rb.AddForce(new Vector3(0, 1f, 0) * jumpPower, ForceMode.Impulse);

        animationManager.ChacngeToJumpAnimation();
        StartCoroutine(waitJumpComplete());

        //
    }
    private void Sliding()
    {
        if (!hasGround)
            return;

        animationManager.ChangeSlidingAnimation();
    }

    private bool canChangeLine = true;

    public void ChangeLine(float xDir)
    {
        if (!canChangeLine)
            return;

        if (xDir < 0 && transform.position.x <= -stepSizeValue)
            return;

        if (xDir > 0 && transform.position.x >= stepSizeValue)
            return;

        canChangeLine = false;
        transform.DOMoveX(transform.position.x + (xDir * stepSizeValue), changeLineSpeed).OnComplete(()=> canChangeLine = true);
        //transform.DOMoveX(xDir * stepSizeValue, changeLineSpeed);
    }

    private IEnumerator waitJumpComplete()
    {
        yield return new WaitForSeconds(0.2f);

        while (!hasGround)
        {
            yield return null;
        }

        animationManager.ChangeToRunAnimation();
        ContinueMove();
        yield return null;
    }

    private void ObstacleTriggerHandler(ObstacleType obstacleType)
    {
        StopMove();

        if(obstacleType == ObstacleType.Default)
        {
            animationManager.ChangeToFallAnimatiob();

            if (lastPreJumpPos != Vector3.zero)
            {
                //transform.DOMoveY(2, 0.25f);
                transform.DOMove(lastPreJumpPos, 0.25f);
            }
            else
            {
                transform.DOMoveY(2, 0.25f);
                transform.DOMoveZ(transform.position.z - 60, 0.25f);
            }
        }
        else if(obstacleType == ObstacleType.Banan)
        {
            //Debug.Log("CALL");
            animationManager.ChacngeToBananFallAnimation();
        }
        else
        {
            animationManager.ChangeToFallAnimatiob();

        }

       

        lastPreJumpPos = Vector3.zero;

        tigerEntityHealth.TakeDamage();
    }


    private void StopMove()
    {
        canInputHadle = false;
        canMoveForward = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    private void AddSize()
    {

        Debug.Log("AddSize");

        GamePlayController.tigerSizePowerValue++;
        Vector3 lastScale = transform.localScale;

        transform.DOScale(lastScale * 1.01f, 1f);

    }
}


