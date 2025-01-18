using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSegmentMonkey : MonoBehaviour
{
    [SerializeField]
    private Monkey monkey;

    //TigerEntityAnimationManager

    private Collider lastCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out TigerEntityColliderManager tiger) && lastCollider != other)
        {
            lastCollider = other;
            monkey.StartAttackWithBanan();
        }
    }
}
