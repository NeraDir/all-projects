using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtAnimationComponent : MonoBehaviour
{
    private Vector3 _needPos;

    private void Start()
    {
        _needPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        StartCoroutine(MoveToDestroy());
    }

    private IEnumerator MoveToDestroy() 
    {
        while (transform.position.y != _needPos.y) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _needPos, 5 * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 5 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
