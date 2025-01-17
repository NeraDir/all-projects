using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsAnimation : MonoBehaviour
{
    private Vector3 beginSacel;
    private Vector3 endSacel;

    private void Start()
    {
        if (Random.Range(0,2) != 0)
        {
            beginSacel = transform.localScale;
            endSacel = transform.localScale * 1.3f;
            transform.localScale = endSacel;
            StartCoroutine(ScaleDown());
        }
        else
        {
            beginSacel = transform.localScale;
            endSacel = transform.localScale * 1.3f;
            StartCoroutine(ScaleUp());
        }
      
    }

    private IEnumerator ScaleUp() 
    {
        float changeSpeed = Random.Range(3f, 10f);
        while (transform.localScale != endSacel) 
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, endSacel, changeSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    private IEnumerator ScaleDown() 
    {
        float changeSpeed = Random.Range(3f, 10f);
        while (transform.localScale != beginSacel)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, beginSacel, changeSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(ScaleUp());
    }
}
