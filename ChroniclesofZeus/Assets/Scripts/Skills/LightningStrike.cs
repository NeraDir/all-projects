using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    private Transform currentTarget;

    private float distance = 200;

    private int maxJumpCount = 10;

    private void Start()
    {
        FindNearTarget();
        StartCoroutine(Go());
    }

    private IEnumerator Go() 
    {
        int countJumps = 0;

        while (countJumps < maxJumpCount)
        {
            if (currentTarget != null)
            {
                if (transform.position != currentTarget.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, 10 * Time.deltaTime);
                }
                else
                {
                    currentTarget = FindNearTarget();
                    countJumps++;
                }
            }
            yield return null;
        }
        Destroy(gameObject);
    }

    private Transform FindNearTarget() 
    {
        Enemy[] tempTarget = FindObjectsOfType<Enemy>();
        for (int i = 0; i < tempTarget.Length; i++)
        {
            if (Vector3.Distance(tempTarget[i].gameObject.transform.position,transform.position) <= distance)
            {
                if (currentTarget == null)
                {
                    return tempTarget[i].transform;
                }
                else
                {
                    if ((currentTarget != tempTarget[i].gameObject.transform))
                    {
                        return tempTarget[i].transform;
                    }
                }
            }
        }

        return null;
    }
}
