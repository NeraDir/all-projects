using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallComponent : MonoBehaviour
{
    private void Start()
    {
        transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, Random.Range(-20, 32), transform.parent.localPosition.z);
    }
}
