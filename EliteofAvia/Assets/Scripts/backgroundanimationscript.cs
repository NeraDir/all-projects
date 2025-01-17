using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundanimationscript : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float speed;

    private RectTransform recter;

    private void Start()
    {
        recter = GetComponent<RectTransform>();
        recter.SetLeft(0);
        recter.SetRight(0);
    }

    private void LateUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log(recter.offsetMin.x);
        if (recter.offsetMin.x <= -1325f)
        {
            recter.SetLeft(0);
            recter.SetRight(0);
        }
    }

}
