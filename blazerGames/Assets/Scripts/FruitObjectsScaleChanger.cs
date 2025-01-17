using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitObjectsScaleChanger : MonoBehaviour
{
    public bool isRotate;

    private IEnumerator Start()
    {
        transform.localScale = Vector3.zero;
        while (transform.localScale != Vector3.one)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one,5 * Time.deltaTime);
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (isRotate)
        {
            transform.Rotate(new Vector3(0, 0, 1), 15 * Time.deltaTime);
        }
    }
}
