using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    [SerializeField]
    private Joystick joystick;
    private Rigidbody myRigidbody;
    private Vector3 direction;

    [SerializeField]
    private float speed;

    private PlayerAnimationController playerAnimationController;

    private void OnEnable()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    private void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
            myRigidbody.velocity = direction;
            transform.rotation = Quaternion.LookRotation(myRigidbody.velocity);
            playerAnimationController.PlayWalkAnimation();
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
            playerAnimationController.PlayIdleAnimation();
        }
    }
}
