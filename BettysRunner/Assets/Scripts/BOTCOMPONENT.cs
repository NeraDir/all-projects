using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTCOMPONENT : MonoBehaviour
{
    [SerializeField] private float _checkDistance = 5f;
    [SerializeField] private LayerMask _stopLayer;
    [SerializeField] private LayerMask _jumpLayer;

    private MOVEMENTCOMPONENT _movement;

    private void Start()
    {
        _movement = GetComponent<MOVEMENTCOMPONENT>();
        StartCoroutine(CheckAndJump());
    }

    private IEnumerator CheckAndJump()
    {
        while (true)
        {
            if (GetObstacle() == 1)
            {
                _movement.Jump();
            }
            else if (GetObstacle() == 2)
            {
                _movement.Stay();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private int GetObstacle()
    {
        Vector3 direction = -Vector3.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, _checkDistance, _jumpLayer))
        {
            Debug.DrawRay(transform.position, direction);
            return 1;
        }
        else if (Physics.Raycast(transform.position, direction, out hit, _checkDistance, _stopLayer))
        {
            Debug.DrawRay(transform.position, direction);
            return 2;
        }
        return 0;
    }
}
