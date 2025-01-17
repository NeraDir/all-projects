using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMotions : MonoBehaviour
{
    [SerializeField]
    private Transform obstacleTransform;

    [SerializeField]
    private Transform firstPoint;
    [SerializeField]
    private Transform secondPoint;

    [SerializeField]
    private float obstacleMoveSpeed;

    private bool state;



    private void OnEnable()
    {
        state = false;

        StartCoroutine(startObstacleMove());
    }

    private IEnumerator startObstacleMove()
    {
        while (true)
        {
            if (state)
            {
                if (obstacleTransform.position == firstPoint.position)
                {
                    state = false;
                }
                else
                {
                    obstacleTransform.position = Vector3.MoveTowards(obstacleTransform.position, firstPoint.position, obstacleMoveSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (obstacleTransform.position == secondPoint.position)
                {
                    state = true;
                }
                else
                {
                    obstacleTransform.position = Vector3.MoveTowards(obstacleTransform.position, secondPoint.position, obstacleMoveSpeed * Time.deltaTime);
                }

            } 

            yield return null;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
