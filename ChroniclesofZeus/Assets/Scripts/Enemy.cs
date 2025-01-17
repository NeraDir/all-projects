using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    private Transform finalPoint;

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private EnemyAnimatorController enemyAnimatorController;

    [SerializeField]
    private float enemyHealth;

    [SerializeField]
    private GameObject exp;

    private bool canMove;

    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
        enemyAnimatorController = GetComponentInChildren<EnemyAnimatorController>();

        canMove = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        finalPoint = target;
    }

    private void FixedUpdate()
    {
        myTransform.LookAt(finalPoint);
        if (canMove)
        {
            myRigidbody.velocity = transform.forward * moveSpeed;
        }

    }

    private bool canTrigger = true;


    public void TakeDamage(float value)
    {
        if (enemyHealth - value > 0)
            enemyHealth -= value;
        else
        {
            Instantiate(exp, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), exp.transform.rotation);
            Destroy(gameObject);

        }

        



    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FinalZone finalZone))
        {
            if (canTrigger)
            {
                canTrigger = false;
                canMove = false;

                enemyAnimatorController.PlayAttackAnimation();

            }
        }
    }












}
