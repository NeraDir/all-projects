using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCamPlayerFollowerComponent : MonoBehaviour
{
    [SerializeField]
    private Transform luckyCamTarget;

    [SerializeField]
    private Vector3 luckyOffset;

    [SerializeField]
    private float luckySpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, luckyCamTarget.position + luckyOffset, luckySpeed * Time.deltaTime);
    }
}
