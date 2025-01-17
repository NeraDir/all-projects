using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{


    private Transform _myTransform;
    private Vector3 maxSize;

    private float lerpSpeed = 0.3f;

    private void OnEnable()
    {
        _myTransform = GetComponent<Transform>();
        maxSize = Vector3.one;
        _myTransform.localScale = Vector3.zero;
        StartCoroutine(changeScale());
    }

    private IEnumerator changeScale()
    {
        while (_myTransform.localScale != maxSize)
        {
            _myTransform.localScale = Vector3.Lerp(_myTransform.localScale, maxSize, lerpSpeed);
            yield return null;
        }
    }

}
