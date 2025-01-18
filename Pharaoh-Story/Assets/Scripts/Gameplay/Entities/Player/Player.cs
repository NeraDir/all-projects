using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick Joystick;
    [SerializeField] private float speed = 5;
    public Spawnmanager managerSp;
    public CameraFollow cameraFallow;
    private Rigidbody _rb;
    private Quaternion lastRotation;
    public Animator animator;
    public delegate void OnKosEnemy();
    public static event OnKosEnemy UpdateTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

        cameraFallow = FindObjectOfType<CameraFollow>();
        cameraFallow._object = gameObject;
        managerSp = FindObjectOfType<Spawnmanager>();
        Joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        if (Joystick.Horizontal != 0 && Joystick.Vertical != 0)
        {
            float _targetRotation = Mathf.Atan2(Joystick.Horizontal, Joystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = GetRotation(transform, new Quaternion(0, _targetRotation, 0, 0));

            _rb.velocity = new Vector3(Joystick.Horizontal * speed, _rb.velocity.y, Joystick.Vertical * speed);

            lastRotation = transform.rotation;
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
            transform.rotation = lastRotation;
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            if (collision.gameObject.GetComponent<Enemy>() == managerSp.currentCell)
            {
                collision.gameObject.GetComponent<Enemy>().isSpawned = false;
                Destroy(collision.gameObject);

                

                if (UpdateTime != null)
                    UpdateTime();
            }
        }
    }

    private Quaternion GetRotation(Transform transform, Quaternion rotation)
    {
        float RotationSmoothTime = 0.001f;
        float _rotationVelocity = 0;
        return Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.y, ref _rotationVelocity,
                RotationSmoothTime), 0);

    }
}
