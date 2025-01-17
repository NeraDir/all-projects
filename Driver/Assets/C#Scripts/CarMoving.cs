using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float sprint;

    private Rigidbody2D carRG;

    [SerializeField] private AnimationCurve accel;
    [SerializeField] private AnimationCurve accelRotate;

    private float speed;
    private int boost = 0;
    float h = 0, v = 0;

    private float timerTireMark = 1f;
    private float maxTimerTireMark;
    private void Start()
    {
        maxTimerTireMark = 10;
        speed = 0f;
        timerTireMark = 1f;
        carRG = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float maxS;
        speed = carRG.velocity.magnitude;
        if (boost > 0)
            maxS = sprint;
        else
            maxS = maxSpeed;


        if (Input.GetAxis("Horizontal") != 0)
        {
            h = Input.GetAxis("Horizontal");
            GameManager.instance.StartRound();
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            v = Input.GetAxis("Vertical");
            GameManager.instance.StartRound();
        }
        
        Vector2 Up = new Vector2(transform.up.x, transform.up.y);
        transform.Rotate(0, 0, (h * speedRotation * -Mathf.Sign(v) * carRG.velocity.magnitude / 10));
        carRG.velocity += Up * v * accel.Evaluate(speed / maxS);

    }
    public void SetH(float val)
    {
        h = val;
        GameManager.instance.StartRound();
    }
    public void SetV(float val)
    {
        v = val;
        GameManager.instance.StartRound();
    }
    public void Boost()
    {
        StartCoroutine(IEBoost());
    }
    IEnumerator IEBoost()
    {
        boost ++;
        yield return new WaitForSeconds(3f);
        boost --;
    }
}
