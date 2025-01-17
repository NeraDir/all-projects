using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 defaultSize;


    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();
        defaultSize = myTransform.localScale;
        myTransform.localScale = Vector3.zero;

        //StartCoroutine(chancgeScale());
    }

    public void ChangeScale()
    {
        StartCoroutine(chancgeScale());
    }

    private IEnumerator chancgeScale()
    {
        while (myTransform.localScale != defaultSize)
        {
            myTransform.localScale = Vector3.Lerp(myTransform.localScale, defaultSize, 0.3f);
            yield return null;
        }
    }

}
