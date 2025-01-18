using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerController : MonoBehaviour
{
    public Joystick js;

    private Rigidbody rb;

    public float speed;

    private Animator animator;

    private Quaternion lastRotatetion;

    public GameObject runEffect;

    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (js.Horizontal != 0 && js.Vertical != 0)
        {
            rb.velocity = new Vector3(js.Horizontal * speed, rb.velocity.y, js.Vertical * speed);

            float _targetRotation = Mathf.Atan2(js.Horizontal, js.Vertical) * Mathf.Rad2Deg;
            transform.rotation = GetRotation(transform, new Quaternion(0, _targetRotation, 0, 0));
            animator.SetBool("JokerStates", true);
            lastRotatetion = transform.rotation;
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                Instantiate(runEffect, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), transform.rotation);
                timer = 0;
            }
        }
        else
        {
            transform.rotation = lastRotatetion;
            animator.SetBool("JokerStates", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NoteComponent note))
        {
            note.Use();
        }
    }

    private Quaternion GetRotation(Transform transform, Quaternion rotation)
    {
        float RotationSmoothTime = 0.04f;
        float _rotationVelocity = 0;
        return Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.y, ref _rotationVelocity,
                RotationSmoothTime), 0);

    }
}
