using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 maxSize;

    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();
        maxSize = myTransform.localScale;
        myTransform.localScale = Vector3.zero;
        StartCoroutine(changeSize());
    }

    private IEnumerator changeSize()
    {
        while (myTransform.localScale != maxSize)
        {
            myTransform.localScale = Vector3.Lerp(myTransform.localScale, maxSize, 0.1f);
            yield return null;
        }
    }


}
