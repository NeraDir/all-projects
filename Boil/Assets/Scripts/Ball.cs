using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody rigidbody;

    public Joystick joystick;
    public float moveXAxisSpeedValue;

    public List<Material> skinsMaterial;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        SetActualSkin();
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        /*
        if(joystick.Horizontal != 0)
        {
            moveXaxis = joystick.Horizontal;
        }
        else
        {
            moveXaxis = 1;
        }
        */


        //rigidbody.AddForce(new Vector3(moveXAxisSpeedValue * joystick.Horizontal, 0, speed), ForceMode.VelocityChange);


        if (joystick.Horizontal != 0)
        {
            rigidbody.velocity = new Vector3(moveXAxisSpeedValue * joystick.Horizontal, rigidbody.velocity.y, speed);
        }
        else
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed);
        }
    
    }


    private void SetActualSkin()
    {
        GetComponent<MeshRenderer>().material = skinsMaterial[Configs.ballSkinIndex];
    }
}
 