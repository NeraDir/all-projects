using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float shootPower;

    private Transform myTransform;
    private Rigidbody2D myRigidbody;

    private Vector3 lastVelocity;


    [SerializeField]
    private Transform lookAtTarget;

    private bool ballIsRotating;


    [Header("Circular Motions Parameters")]
    [SerializeField]
    private float speed;
    
    private float defaultTriggerRadius;
    private Coroutine circularMotionsCor;


    private BallRotateTrigger lastRotateTrigger;


    public delegate void BallPointDetecter(Transform point);
    public static event BallPointDetecter BallOnLastPoint;
    public static event BallPointDetecter BallOnFirstPoint;


    public delegate void ObstacleDetecter();
    public static event ObstacleDetecter ObstacleDetected;



    private void OnEnable()
    {
        ballIsRotating = false;

        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();

        detectWallCount = 0;

        InputManager.ScreenTouchDetected += ShootBall;

       
        
    }
    private void OnDisable()
    {
        InputManager.ScreenTouchDetected -= ShootBall;
    }



    private void FixedUpdate()
    {
        lastVelocity = myRigidbody.velocity;

       
    }



    private IEnumerator circularMotions()
    {
        ballIsRotating = true;
        myRigidbody.velocity = Vector2.zero;

        while (true)
        {
            myTransform.RotateAround(lookAtTarget.position, new Vector3(0, 0, 1), speed * Time.deltaTime);


            Vector3 rotation = lookAtTarget.position - myTransform.position;

            float rotateZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            myTransform.rotation = Quaternion.Euler(0, myTransform.rotation.y, rotateZ);



            yield return null;
        }
    }

    public void ShootBall()
    {
        if (ballIsRotating)
        {
            ballIsRotating = false;
            myRigidbody.simulated = true;
            //myTransform.parent = defaultBallParent;
            //myRigidbody.AddForce(-myTransform.up * shootPower, ForceMode2D.Impulse);

            Vector3 direction = -myTransform.right;

            myRigidbody.AddForce(direction * shootPower, ForceMode2D.Impulse);
            StopCoroutine(circularMotionsCor);

            //Invoke(nameof(ResetLastRotationTrigger), 0.8f);

           // Debug.Log("Shoot");
        }
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BallRotateTrigger rotateTrigger))
        {
            if (!rotateTrigger.hasBall)
            {
                lastRotateTrigger = rotateTrigger;

                defaultTriggerRadius = lastRotateTrigger.myCircleCollider2D.radius;
               

                rotateTrigger.hasBall = true;

                if (myRigidbody.simulated)
                {
                    myRigidbody.simulated = false;

                    lookAtTarget = rotateTrigger.transform;

                    lastRotateTrigger.DisableCollider();

                    circularMotionsCor = StartCoroutine(circularMotions());

                    detectWallCount = 0;

                    if (lastRotateTrigger.isLast)
                    {
                        if (BallOnLastPoint != null)
                        {
                            BallOnLastPoint(lastRotateTrigger.ballDetecterTriggerPoint);
                        }
                    }

                    if (lastRotateTrigger.isFirst)
                    {
                        if (BallOnFirstPoint != null)
                        {
                            BallOnFirstPoint(lastRotateTrigger.parentPoint);
                        }
                    }
                }


            }

        }

        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {

            if (ObstacleDetected != null)
            {
                ObstacleDetected();
            }

            if (BallConfigsController.ballHealth >= 1)
            {
                myRigidbody.velocity = Vector2.zero;
                myTransform.position = lastRotateTrigger.ballRespawnPoint.position;
            }
            else
            {
               // Destroy(gameObject);
            }


            //Debug.Log("GOVNO");
        }

        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
            BallConfigsController.coinCount++;
        }


    }

    /*
    private void ResetLastRotationTrigger()
    {
        if (lastRotateTrigger != null)
        {
            lastRotateTrigger.myCircleCollider2D.enabled = true;
            lastRotateTrigger.myCircleCollider2D.radius = defaultTriggerRadius;
            lastRotateTrigger.hasBall = false;
            lastRotateTrigger = null;
            
        }
    }
    */

    private int detectWallCount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            myRigidbody.velocity = direction * Mathf.Max(speed, 0);
            detectWallCount++;

            if (detectWallCount == 2)
            {
                detectWallCount = 0;

                if (ObstacleDetected != null)
                {
                    ObstacleDetected();
                }

                if (BallConfigsController.ballHealth >= 1)
                {
                    myRigidbody.velocity = Vector2.zero;
                    myTransform.position = lastRotateTrigger.ballRespawnPoint.position;
                }

            }

            //myRigidbody.velocity -= (Vector2.one * 0.5f);
        }


    }
}
