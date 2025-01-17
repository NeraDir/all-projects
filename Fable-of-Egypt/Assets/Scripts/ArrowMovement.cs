using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve fallNukeCurve;

    [SerializeField] private float speed;
    [SerializeField] private bool isMove;
    
    private float currentTime;
    private bool right;

    public void Movement(bool isMove) => this.isMove = isMove;

    private void FixedUpdate()
    {
        if (!isMove) return;

        transform.position = new Vector3(fallNukeCurve.Evaluate(currentTime), transform.position.y, transform.position.z);

        if (currentTime > 1f)
        {
            right = false;
        }
        else if (currentTime < 0)
        {
            right = true;
        }

        if (right)
        {
            currentTime += Time.deltaTime * speed / 100f;
        }
        else
        {
            currentTime -= Time.deltaTime * speed / 100f;
        }

    }


}
