using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.up, 605f * Time.deltaTime);
    }
}
